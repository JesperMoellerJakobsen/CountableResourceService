using System.ComponentModel.DataAnnotations;

namespace API.Model
{
    public class CounterPatchInput
    {
        [Required]
        public CounterPatchOption PatchOption { get; set; }

        [Required]
        public byte[] Version { get; set; }
    }
}
