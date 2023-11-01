using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot_project.Model;

public class Hero
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("localized_name")]
    public string Name { get; set; }

    [JsonProperty("attack_type")]
    public string AttackType { get; set; }

    [JsonProperty("roles")]
    public string[] Roles { get; set; }

}