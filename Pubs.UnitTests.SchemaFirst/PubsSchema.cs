using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GraphQL;
using GraphQL.Types;
using Pubs.SchemaFirst;

namespace Pubs.UnitTests.SchemaFirst
{
    public class PubsSchema
    {
        // it appears that it has to be type Query with a class name named Query?

        public const string Schema = @"
                type Author
                {
                    authorId: ID!
                    lastName: String
                    firstName: String
                    phone: String
                    address: String
                    city: String
                    state: String
                    zip: String
                    contract: Boolean

                    titles: [Title!]
                }

                type Publisher
                {
                    pubId:      ID!
                    name:       String
                    city:       String
                    state:      String
                    country:    String

                    titles: [Title!]
                }

                type Title
                {
                    titleId: ID!
                    title: String
                    pubId: String
                    type: String
                    pubDate: String
                    notes: String
                    price: Float
                    advance: Float
                    royalty: Int
                    ytdSales: Int

                    publisher : Publisher

                    authors : [Author!]
                 }

                type Query
                {
                    author(id: ID!)     : Author
                    authors             : [Author!]
                    publisher(id: ID!)  : Publisher
                    publishers          : [Publisher!]
                    title(id: ID!)      : Title
                    titles              : [Title!]
                }
            ";

        // https://graphql-dotnet.github.io/docs/getting-started/introduction/
        // Schema First Nested Types
        public static ISchema GetSchema()
        {
            ValueConverter.Register(
                  typeof(decimal),
                  typeof(double),
                  value => Convert.ToDouble((decimal)value, NumberFormatInfo.InvariantInfo));
    
            return GraphQL.Types.Schema.For(PubsSchema.Schema, _ =>
            {
                _.Types.Include<AuthorType>();
                _.Types.Include<PublisherType>();
                _.Types.Include<TitleType>();
                _.Types.Include<Query>();
            });
        }
    }
}
