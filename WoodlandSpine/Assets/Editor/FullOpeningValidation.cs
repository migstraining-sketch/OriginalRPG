using System;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class FullOpeningValidation
    {
        static int checks;
        static void Check(bool value,string label){if(!value)throw new Exception("Opening validation: "+label);checks++;}
        public static void Run()
        {
            checks=0;
            for(int route=0;route<3;route++)for(int lethal=0;lethal<2;lethal++)
            {
                var p=new OpeningProgress();Check(p.Choose(route),"choose posting");Check(!p.Choose((route+1)%3),"one active contract");var h=p.hunts[route];
                Check(!h.Resolve(true),"kill cannot substitute for investigation");Check(!h.Interpret(true),"evidence required");
                for(int i=0;i<4;i++)h.Inspect(i);Check(h.AllEvidence,"all evidence");Check(!h.Interpret(false),"wrong interpretation rejected");Check(h.Interpret(true),"correct interpretation");Check(!h.Resolve(false),"follow evidence before resolution");h.followed=true;
                Check(h.Resolve(lethal==1),"both routes resolve");Check(!p.huntingLearned,"unlock waits for client confirmation");Check(p.Reward(route,"Food"),"client reward");Check(!p.Reward(route,"Food"),"no duplicate reward");Check(p.foodPortions==3&&p.coins==25&&p.huntingLearned,"edible outcome and reward");Check(p.Choose((route+1)%3),"other postings remain available");
            }
            var time=new OpeningProgress{illnessSeconds=10};time.TickIllness(11,false,false);Check(!time.marlowGone,"clock waits for invitation");time.TickIllness(9,true,false);Check(!time.marlowGone,"reconsider window");time.TickIllness(2,true,false);Check(time.marlowGone,"real consequence");
            var treated=new OpeningProgress{illnessSeconds=1};treated.TickIllness(30,true,true);Check(!treated.marlowGone,"treatment stops clock");
            var room=new OpeningProgress();Check(!room.Rent(),"room affordability");room.coins=18;Check(room.Rent()&&room.coins==0,"room exact price");Check(!room.Rent(),"no double charge");
            foreach(string food in new[]{"Fresh Duskhen Eggs","Fresh Reedback Haunch","Preserved Reedback Cut","Naturally Shed Brookmaw Tail"})
            {
                var c=new CookingSession{ingredient=food};while(c.step<CookStep.Cook)c.Advance();c.heat=1;Check(!c.Advance()&&c.step==CookStep.Cook,"supervised heat error");c.heat=c.Eggs?.35f:.55f;while(c.step<CookStep.Complete)c.Advance();Check(!c.Advance(),"finished cooking idempotent");
            }
            Debug.Log("FULL_OPENING_RULES_SUCCESS: "+checks);
        }
    }
}
