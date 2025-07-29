using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.BusinessLogicLayer.Entity;

public class OrderItem
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid _id { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public Guid ProductID { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.Decimal128)]
    public decimal UnitPrice { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.Int64)]
    public int Quantity { get; set; }

    [BsonRepresentation(MongoDB.Bson.BsonType.Double)]
    public decimal TotalPrice { get; set; }
}
