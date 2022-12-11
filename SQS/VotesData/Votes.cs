using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace VotesData;

public class Votes
{
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int VoteCount { get; set; }

}