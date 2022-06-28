﻿using System.ComponentModel.DataAnnotations;

namespace CommandService.Model
{
    public class Command
    {
        [Key]
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string HowTo { get; set; }
        
        [Required]
        public string CommandLine { get; set; }
        
        [Required]
        public int PlatformId { get; set; }
        
        
        public Platform Platform { get; set; }
    }
}
