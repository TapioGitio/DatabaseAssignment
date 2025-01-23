﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectManagerEntity
{
    [Key]
    public int Id { get; set; }

    [Column (TypeName = "nvarchar(50)")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "nvarchar(50)")]
    public string LastName { get; set; } = null!;

    [Column (TypeName = "varchar(20)")]
    public string PhoneNumber { get; set; } = null!;

    public ICollection<ProjectEntity> projects { get; set; } = null!;

}
