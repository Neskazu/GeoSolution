using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.Models
{
    [Table("gis_osm_buildings_a_free_1")]
    public class GisOsmBuildingModel
    {
        [Key]
        [Column("gid")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("geom")]
        public string Geometry { get; set; }
        [Column("fclass")]
        public string Fclass { get; set; }
        [Column("type")]
        public string Type { get; set; } 

    }
}
