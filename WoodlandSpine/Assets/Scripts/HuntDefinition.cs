using System;
namespace WoodlandSpine
{
    [Serializable] public sealed class HuntDefinition
    {
        public string title, client, location, problem, creature, deduction, wrongDeduction, solution, freshFood, peacefulFood;
        public string[] clues;
        public static HuntDefinition[] Defaults()=>new[]{
            new HuntDefinition{title="Mud in the Moonrice",client="Toma Reed",location="West paddies — west fork beyond the inn",problem="Something tears up the paddies overnight.",creature="Reedback",deduction="It digs for mudgrubs beneath the rice.",wrongDeduction="It is eating the rice plants.",solution="Move mudgrubs to the unused wet bed and open a route away from the rice",freshFood="Fresh Reedback Haunch",peacefulFood="Preserved Reedback Cut",clues=new[]{"Broad tracks sink deep into the wet bank.","Rice has been uprooted, but the stems are uneaten.","Broken mudgrub casings lie among the roots.","A broken bank connects the paddy to an unused wet bed."}},
            new HuntDefinition{title="Three Missing by Morning",client="Mara Venn",location="East coop — east fork beyond the inn",problem="Duskhen disappear at night although the coop door stays shut.",creature="Nightquill",deduction="It glides from the overhanging branch and enters above.",wrongDeduction="It forces the door open from the ground.",solution="Close the roof gap and cut back the overhanging glide route",freshFood="Fresh Duskhen Eggs",peacefulFood="Fresh Duskhen Eggs",clues=new[]{"The door and ground-level latch are intact.","A long quill is caught well above head height.","Fine scratches lead along the roof edge.","A low limb overhangs the roof; there are no tracks at the door."}},
            new HuntDefinition{title="When the Wheel Stopped",client="Oren Vale",location="Riverside mill — farther along the east fork",problem="The mill channel keeps filling with carefully packed branches.",creature="Brookmaw",deduction="The blocked channel is a nursery; the old side-channel is safer.",wrongDeduction="Random storm debris is collecting here.",solution="Clear the old side-channel and guide the nursery into sheltered water",freshFood="Fresh Brookmaw Tail",peacefulFood="Naturally Shed Brookmaw Tail",clues=new[]{"Branches have been deliberately woven, not merely washed downstream.","Drag marks run from the bank into shallow water.","Small sheltered hollows sit behind the branch barrier.","An old side-channel is clogged with loose stones; quieter water lies beyond."}}
        };
    }
}
