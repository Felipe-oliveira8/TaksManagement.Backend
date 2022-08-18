using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaksManagement.Backend.Entities.MongoEntities
{
    public class UserEntity : MongoEntity
    {

        public string Name { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string CPF { get; set; }

        public string Tel { get; set; }

        public string Email { get; set; }

    }
}
