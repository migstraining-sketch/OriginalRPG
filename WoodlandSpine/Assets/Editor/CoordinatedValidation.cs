using System;
using UnityEngine;

namespace WoodlandSpine.Editor
{
    public static class CoordinatedValidation
    {
        static int count;
        static void Check(bool value,string message){if(!value)throw new Exception("Coordinated slice: "+message);count++;}
        public static void Run(SliceData rules)
        {
            count=0;
            var travel=new TravelKnowledge();Check(travel.Knows(Region.Inn)&&!travel.Knows(Region.Woodland)&&!travel.Knows(Region.Reedwater),"only starting location known");
            travel.Discover(Region.Woodland);Check(travel.CanTravel(Region.Woodland)&&!travel.CanTravel(Region.Reedwater),"learning Woodland does not reveal contract destination");
            travel.Discover(Region.Reedwater);travel.current=Region.Woodland;Check(travel.CanTravel(Region.Reedwater)&&travel.CanTravel(Region.Inn),"independent regional routes");
            for(int first=0;first<3;first++)for(int second=0;second<3;second++)if(first!=second)
            {
                var mud=new MudInvestigation();mud.Inspect(first);Check(!mud.Inferred,"one clue not inference");mud.Inspect(second);Check(mud.Inferred,"any two clues yield inference");Check(mud.GrubsInferred==(first==1||second==1),"explicit grub inference needs grub observation");
            }
            var early=new MudInvestigation{patchOpen=true,runnelOpen=true};Check(early.ObserveFeeding()&&early.Complete&&!early.Inferred,"early prepared patch can complete through observation");
            early.Killed();Check(!early.Complete&&early.Harvest()&&early.Complete,"kill after redirect changes final route and requires harvest");Check(!early.Harvest(),"harvest once");
            var incomplete=new MudInvestigation{patchOpen=true};Check(!incomplete.ObserveFeeding()&&!incomplete.Complete,"patch alone is not redirection");
            var herd=new HerdState{provoked=true};Check(!herd.SourceLost,"aggression is not failure");Check(!herd.Collect(false)&&herd.Collect(true),"living source needs clean flask");herd.sourceDead=true;Check(!herd.SourceLost,"milk acquired before source loss remains valid");
            var lost=new HerdState{sourceDead=true};Check(lost.SourceLost&&!lost.Collect(true),"dead source cannot produce milk");
            var bag=new Inventory{bandages=3,cleanFieldFlask=true};bag.Receive(rules.weapons[2]);bag.Store(rules.coat);bag.body=rules.coat;bag.provisions["Fresh Reedback Haunch"]=3;var chest=new Inventory();
            foreach(var entry in InventoryItems.List(bag))
            {
                int before=entry.Count(bag);Check(InventoryItems.Transfer(bag,chest,entry,before),"store "+entry.name);Check(entry.Count(bag)==0&&entry.Count(chest)==before,"conserve stored "+entry.name);
            }
            Check(bag.weapon==null&&bag.body==null,"storing equipped item explicitly unequips it");
            foreach(var entry in InventoryItems.List(chest)){int before=entry.Count(chest);Check(InventoryItems.Transfer(chest,bag,entry,before)&&entry.Count(bag)==before,"withdraw preserves same item identity/count");}
            Check(bag.weapons.Contains(rules.weapons[2])&&bag.bodies.Contains(rules.coat),"equipment references survive transfer");
            var storage=new RoomStorage{visible=true};storage.pending=InventoryItems.List(bag)[0];storage.Cancel();Check(storage.visible&&storage.pending==null,"Back cancels only unfinished quantity");storage.Cancel();Check(!storage.visible&&bag.bandages==3,"close does not reverse committed transfers");
            var root=new GameObject("Dialogue Back validation");try
            {
                var game=root.AddComponent<SliceGame>();bool accepted=false;game.Talk("Marlow","Will you help?",new DialogueChoice("I'll help.",()=>accepted=true));var decision=game.dialogue;game.Back();Check(game.dialogue==decision&&game.mode==GameMode.Dialogue&&!accepted,"Esc keeps decision visible and does not choose");
            }finally{UnityEngine.Object.DestroyImmediate(root);}
            Debug.Log("COORDINATED_VALIDATION_PASSED: "+count);
        }
    }
}
