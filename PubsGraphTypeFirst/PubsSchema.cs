using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pubs.Types
{
    public class PubsSchema : Schema
    {
        public PubsSchema(IDependencyResolver resolver)
            : base(resolver)
        {
            Query = resolver.Resolve<AuthorsQuery>();
            //Mutation = resolver.Resolve<StarWarsMutation>();
        }
    }
}
