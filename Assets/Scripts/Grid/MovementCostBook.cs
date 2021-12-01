using System.Collections.Generic;

public struct MovementCostBook
{
    public static Dictionary<MovementTypes, Dictionary<TerrainTypes, float>> Lookup = new Dictionary<MovementTypes, Dictionary<TerrainTypes, float>> 
    {
        [MovementTypes.foot] = new Dictionary<TerrainTypes, float> 
            {
                  [TerrainTypes.fast_path] = 0.5f
                , [TerrainTypes.heal] = 1
                , [TerrainTypes.out_path] = 1
                , [TerrainTypes.in_path] = 1
                , [TerrainTypes.shop] = 1
                , [TerrainTypes.armory] = 1
                , [TerrainTypes.trap] = 1
                , [TerrainTypes.lucky] = 1
                , [TerrainTypes.boss] = 1
                , [TerrainTypes.in_evade] = 1.5f
                , [TerrainTypes.out_evade] = 1.5f
                , [TerrainTypes.defense] = 2
                , [TerrainTypes.slow_path] = 2
                , [TerrainTypes.out_wall] = 5
                , [TerrainTypes.in_wall] = int.MaxValue
                , [TerrainTypes.gap] = int.MaxValue
            },
        [MovementTypes.foot_light] = new Dictionary<TerrainTypes, float> 
            {
                  [TerrainTypes.fast_path] = 0.5f
                , [TerrainTypes.heal] = 1
                , [TerrainTypes.out_path] = 1
                , [TerrainTypes.in_path] = 1
                , [TerrainTypes.shop] = 1
                , [TerrainTypes.armory] = 1
                , [TerrainTypes.trap] = 1
                , [TerrainTypes.lucky] = 1
                , [TerrainTypes.boss] = 1
                , [TerrainTypes.slow_path] = 1
                , [TerrainTypes.in_evade] = 1.5f
                , [TerrainTypes.out_evade] = 1.5f
                , [TerrainTypes.defense] = 1.5f
                , [TerrainTypes.out_wall] = 3
                , [TerrainTypes.in_wall] = int.MaxValue
                , [TerrainTypes.gap] = int.MaxValue
            },
        [MovementTypes.foot_heavy] = new Dictionary<TerrainTypes, float> 
            {
                  [TerrainTypes.fast_path] = 0.5f
                , [TerrainTypes.heal] = 1
                , [TerrainTypes.out_path] = 1
                , [TerrainTypes.in_path] = 1
                , [TerrainTypes.shop] = 1
                , [TerrainTypes.armory] = 1
                , [TerrainTypes.trap] = 1
                , [TerrainTypes.lucky] = 1
                , [TerrainTypes.boss] = 1
                , [TerrainTypes.in_evade] = 1.5f
                , [TerrainTypes.out_evade] = 1.5f
                , [TerrainTypes.defense] = 2
                , [TerrainTypes.slow_path] = 2
                , [TerrainTypes.out_wall] = 5
                , [TerrainTypes.in_wall] = int.MaxValue
                , [TerrainTypes.gap] = int.MaxValue
            },
        [MovementTypes.mount] = new Dictionary<TerrainTypes, float> 
            {
                  [TerrainTypes.fast_path] = 0.5f
                , [TerrainTypes.heal] = 1
                , [TerrainTypes.out_path] = 1
                , [TerrainTypes.shop] = 1
                , [TerrainTypes.armory] = 1
                , [TerrainTypes.trap] = 1
                , [TerrainTypes.lucky] = 1
                , [TerrainTypes.boss] = 1
                , [TerrainTypes.in_path] = 1.5f
                , [TerrainTypes.in_evade] = 2.5f
                , [TerrainTypes.out_evade] = 2
                , [TerrainTypes.defense] = 2
                , [TerrainTypes.slow_path] = 4
                , [TerrainTypes.out_wall] = int.MaxValue
                , [TerrainTypes.in_wall] = int.MaxValue
                , [TerrainTypes.gap] = int.MaxValue
            },
        [MovementTypes.flying] = new Dictionary<TerrainTypes, float> 
            {
                  [TerrainTypes.fast_path] = 1
                , [TerrainTypes.heal] = 1
                , [TerrainTypes.out_path] = 1
                , [TerrainTypes.shop] = 1
                , [TerrainTypes.armory] = 1
                , [TerrainTypes.trap] = 1
                , [TerrainTypes.lucky] = 1
                , [TerrainTypes.boss] = 1
                , [TerrainTypes.out_evade] = 1
                , [TerrainTypes.defense] = 1
                , [TerrainTypes.slow_path] = 1
                , [TerrainTypes.out_wall] = 1
                , [TerrainTypes.gap] = 1
                , [TerrainTypes.in_path] = 3
                , [TerrainTypes.in_evade] = 3
                , [TerrainTypes.in_wall] = int.MaxValue
            },
    };
}