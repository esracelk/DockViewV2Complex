// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazorApp.Server.Data;


public class Foo
{
  
    [Display(Name = "主键")]
    [AutoGenerateColumn(Ignore = true)] //tabloya yansımaz
    public int Id { get; set; }

    [Required(ErrorMessage = "{0}不能为空")]
    [AutoGenerateColumn(Order = 10, Filterable = true, Searchable = true)]
    [Display(Name = "姓名")]
    public string? Name { get; set; }

    [AutoGenerateColumn(Order = 1, FormatString = "yyyy-MM-dd", Width = 180)]
    [Display(Name = "日期")]
    public DateTime DateTime { get; set; }

  
    [Display(Name = "地址")]
    [Required(ErrorMessage = "{0}不能为空")]
    [AutoGenerateColumn(Order = 20, Filterable = true, Searchable = true)]
    public string? Address { get; set; }

   
    [Display(Name = "数量")]
    [Required]
    [AutoGenerateColumn(Order = 40, Sortable = true)]
    public int Count { get; set; }

    
    [Display(Name = "是/否")]
    [AutoGenerateColumn(Order = 50, ComponentType = typeof(Switch))]
    public bool Complete { get; set; }

   
    [Required(ErrorMessage = "请选择学历")]
    [Display(Name = "学历")]
    [AutoGenerateColumn(Order = 60)]
    public EnumEducation? Education { get; set; }

   
    [Required(ErrorMessage = "请选择一种{0}")]
    [Display(Name = "爱好")]
    [AutoGenerateColumn(Order = 70)]
    public IEnumerable<string> Hobby { get; set; } = [];

    private static readonly Random random = new();

  //random verilerle foo objesi olusturulur
    public static Foo Generate(IStringLocalizer<Foo> localizer) => new()
    {
        Id = 1,
        Name = localizer["Foo.Name", "1000"],
        DateTime = DateTime.Now,
        Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
        Count = random.Next(1, 100),
        Complete = random.Next(1, 100) > 50,
        Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
    };

    //uretilen objeler liste atılır

    public static List<Foo> GenerateFoo(IStringLocalizer<Foo> localizer, int count = 80) => Enumerable.Range(1, count).Select(i => new Foo()
    {
        Id = i,
        Name = localizer["Foo.Name", $"{i:d4}"],
        DateTime = DateTime.Now.AddDays(i - 1),
        Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
        Count = random.Next(1, 100),
        Complete = random.Next(1, 100) > 50,
        Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
    }).ToList();

   
    public static IEnumerable<SelectedItem> GenerateHobbies(IStringLocalizer<Foo> localizer) => localizer["Hobbies"].Value.Split(",").Select(i => new SelectedItem(i, i)).ToList();
}


public enum EnumEducation
{

    [Display(Name = "小学")]
    Primary,

 
    [Display(Name = "中学")]
    Middle
}
