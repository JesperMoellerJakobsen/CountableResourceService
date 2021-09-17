using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class CounterPatchInput
    {
        [Required]
        public byte[] Version { get; set; }

        [Required]
        public CounterPatchOption PatchOption { get; set; }
    }
}
