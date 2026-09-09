using System;
using UnityEditor;
using UnityEngine;
namespace WoodlandSpine.Editor
{
    public static class ReactiveIntroValidation
    {
        static int count;
        static void Check(bool result,string label){if(!result)throw new Exception("Intro state: "+label);count++;}
        [MenuItem("Woodland/Validate reactive introduction")]
        public static void Run()
        {
            count=0;
            foreach(FirstApproach approach in Enum.GetValues(typeof(FirstApproach)))
            {
                if(approach==FirstApproach.None)continue;
                var state=new ReactiveIntroState();state.Begin(approach);Check(state.first==approach&&state.beat==IntroBeat.Reaction,"context captured "+approach);
                state.Begin(FirstApproach.Garrick);Check(state.first==approach,"context retained on resume "+approach);
                string text=state.Welcome("Traveller");
                if(approach==FirstApproach.Rooms)Check(!text.Contains("Rooms upstairs"),"rooms redundancy removed");
                if(approach==FirstApproach.Board)Check(!text.Contains("Work comes through"),"board redundancy removed");
                if(approach==FirstApproach.TakeMerchandise)Check(state.theftWarned,"theft state hook");
                Check(state.Welcome("Traveller")==text,"welcome stable on resume");
            }
            var s=new ReactiveIntroState{merchandiseKnown=true};s.Begin(FirstApproach.Kitchen);Check(s.merchandiseKnown&&!s.Welcome("Traveller").Contains("decent blade"),"looked-at stock retained across another branch");
            s.Refuse();Check(s.offerRefused&&s.refusals==1,"refusal consequence hook");s.Accept();Check(!s.offerRefused&&s.refusals==1,"reconsideration retains history");
            Check(IntroBeat.Invitation<IntroBeat.Warning,"invitation precedes warning");
            Debug.Log($"REACTIVE_INTRO_VALIDATION_PASSED: {count} assertions");
        }
    }
}
