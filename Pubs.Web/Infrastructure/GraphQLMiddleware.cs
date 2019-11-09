using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Http;
using GraphQL.Instrumentation;
using GraphQL.Types;
using GraphQL.Validation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pubs.Types;
//using StarWars;

namespace Pubs.Infrastructure
{
    public class GraphQLMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly GraphQLSettings _settings;
        private readonly IDocumentExecuter _executer;
        private readonly IDocumentWriter _writer;

        public GraphQLMiddleware(
            RequestDelegate next,
            GraphQLSettings settings,
            IDocumentExecuter executer,
            IDocumentWriter writer)
        {
            _next = next;
            _settings = settings;
            _executer = executer;
            _writer = writer;
        }

        public async Task Invoke(HttpContext context, ISchema schema)
        {
            if (!IsGraphQLRequest(context))
            {
                await _next(context);
                return;
            }

            await ExecuteAsync(context, schema);
        }

        private bool IsGraphQLRequest(HttpContext context)
        {
            return context.Request.Path.StartsWithSegments(_settings.Path)
                && string.Equals(context.Request.Method, "POST", StringComparison.OrdinalIgnoreCase);
        }

        private async Task ExecuteAsync(HttpContext context, ISchema schema)
        {
            var request = Deserialize<GraphQLRequest>(context.Request.Body);

            // The following is a work around due to the lack of a Root Query.
            // However, while an Operation Name shouldn't be required, it may be useful for performance optimization / solving the N+1 problem.
            // Instead of multiple queries to obtain data, a single query might be much faster than multiple queries.

            // A Root Query is the recommended approach.
            // https://graphql-dotnet.github.io/docs/getting-started/query-organization/

            /*
                The N + 1 problem occurs when an application gets data from the database, and then loops through the result of that data. 
                That means we call to the database again and again and again. In total, the application will call the database once for 
                every row returned by the first query (N) plus the original query ( + 1).
                https://www.brentozar.com/archive/2018/07/common-entity-framework-problems-n-1/

                https://stackoverflow.com/questions/97197/what-is-the-n1-selects-problem-in-orm-object-relational-mapping
             */

            switch (request?.OperationName)
            {
                case "AuthorQuery":
                    schema = new Schema { Query = new AuthorQuery() };
                    break;
                case "AuthorsQuery":
                default:
                    schema = new Schema { Query = new AuthorsQuery() };
                    break;
                case "PublisherQuery":
                    schema = new Schema { Query = new PublisherQuery() };
                    break;
                case "PublishersQuery":
                    schema = new Schema { Query = new PublishersQuery() };
                    break;
                case "TitleQuery":
                    schema = new Schema { Query = new TitleQuery() };
                    break;
                case "TitlesQuery":
                    schema = new Schema { Query = new TitlesQuery() };
                    break;
            }

            var result = await _executer.ExecuteAsync(_ =>
            {
                _.Schema = schema;
                _.Query = request?.Query;
                _.OperationName = request?.OperationName;
                _.Inputs = request?.Variables.ToInputs();
                _.UserContext = _settings.BuildUserContext?.Invoke(context);
                //_.ValidationRules = DocumentValidator.CoreRules().Concat(new [] { new InputValidationRule() });
                _.EnableMetrics = _settings.EnableMetrics;
                if (_settings.EnableMetrics)
                {
                    _.FieldMiddleware.Use<InstrumentFieldsMiddleware>();
                }
            });

            await WriteResponseAsync(context, result);
        }

        private async Task WriteResponseAsync(HttpContext context, ExecutionResult result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;

            await _writer.WriteAsync(context.Response.Body, result);
        }

        public static T Deserialize<T>(Stream s)
        {
            using (var reader = new StreamReader(s))
            using (var jsonReader = new JsonTextReader(reader))
            {
                var ser = new JsonSerializer();
                var deser = ser.Deserialize<T>(jsonReader);
                return deser;
            }
        }
    }
}
