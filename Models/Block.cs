using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace iTech.Models
{

    public partial class Block
    {
        public Block()
        {


        }

        [Key]
        public int BlockId { get; set; }


        [StringLength(50)]
        public string BlockTitleAr { get; set; }


        [StringLength(50)]
        public string BlockTitleEn { get; set; }

        public string BlockPic { get; set; }

        public bool? BlockIsActive { get; set; }

        public string BlockRemarks { get; set; }
        public string BlockCategory { get; set; }
        public string BlockContent { get; set; }


    }

}
