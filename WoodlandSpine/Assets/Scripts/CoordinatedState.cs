using System;
using System.Collections.Generic;

namespace WoodlandSpine
{
    public enum Region { Inn, Woodland, Reedwater }
    public sealed class TravelKnowledge
    {
        readonly HashSet<Region> known=new HashSet<Region>{Region.Inn};
        public Region current=Region.Inn;
        public void Discover(Region region){known.Add(region);}
        public bool Knows(Region region)=>known.Contains(region);
        public bool CanTravel(Region region)=>region!=current&&Knows(region);
    }
    public sealed class MudInvestigation
    {
        public int evidence,trail;
        public bool patchOpen,runnelOpen,dead,harvested,feedingObserved;
        public int EvidenceCount => ((evidence&1)>0?1:0)+((evidence&2)>0?1:0)+((evidence&4)>0?1:0);
        public bool Inferred=>EvidenceCount>=2;
        public bool GrubsInferred=>Inferred&&(evidence&2)>0;
        public bool Prepared=>patchOpen&&runnelOpen;
        public bool Complete=>dead?harvested:feedingObserved;
        public void Inspect(int index){if(index>=0&&index<3)evidence|=1<<index;}
        public void Track(int index){if(index>=0&&index<3)trail|=1<<index;}
        public void Killed(){dead=true;feedingObserved=false;}
        public bool Harvest(){if(!dead||harvested)return false;harvested=true;return true;}
        public bool ObserveFeeding(){if(dead||!Prepared||feedingObserved)return false;feedingObserved=true;return true;}
    }
    public sealed class HerdState
    {
        public bool provoked,milkTaken,sourceDead,failureReported;
        public bool SourceLost=>sourceDead&&!milkTaken;
        public bool Collect(bool cleanFlask){if(!cleanFlask||sourceDead||milkTaken)return false;milkTaken=true;return true;}
    }
}
