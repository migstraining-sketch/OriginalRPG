using System;
namespace WoodlandSpine
{
    public enum MarlowInterest { Unknown, WillingToHear, RefusedToHear }
    public enum MarlowJob { NotOffered, Offered, Accepted, Refused }
    public enum FirstApproach { None, Garrick, Kitchen, Rooms, Basement, Board, Merchandise, TakeMerchandise, Marlow, Idle }
    public enum IntroBeat { NotStarted, Reaction, Name, Welcome, Sample, WatchRequest, WatchReply, PairOfLegs, Stranger, Nope, Invitation, Warning, Finished }
    [Serializable]
    public sealed class ReactiveIntroState
    {
        public bool playerNameKnown,paymentAsked,paymentKnown,woodlandKnown,ingredientsKnown,symptomsKnown,rescueKnown,cannotLeaveKnown,watchKnown,kitchenKnown;
        public MarlowInterest marlowInterest;
        public MarlowJob marlowJob;
        public string labCheckpoint;
        public FirstApproach first;
        public IntroBeat beat;
        public bool roomsKnown, boardKnown, merchandiseKnown, theftWarned;
        public bool leftBeforeIntroduction, returnedAfterLeaving, exitRemarkMade, idleAcknowledged;
        public bool sampleBroken, warnedAfterInvitation, offerRefused;
        public int refusals;
        public string node;
        public bool interestedInWork, marlowIntroduced, basementTried;
        public int basementAttempts;
        public string welcome;
        public void Begin(FirstApproach approach)
        {
            if(beat!=IntroBeat.NotStarted)return;
            first=approach;beat=IntroBeat.Reaction;
            roomsKnown|=approach==FirstApproach.Rooms;
            boardKnown|=approach==FirstApproach.Board;
            merchandiseKnown|=approach==FirstApproach.Merchandise||approach==FirstApproach.TakeMerchandise;
            theftWarned|=approach==FirstApproach.TakeMerchandise;
        }
        public string Welcome(string name)
        {
            if(!string.IsNullOrEmpty(welcome))return welcome;
            welcome=$"Right, {name}.";
            if(!roomsKnown)welcome+=" Rooms upstairs if you're staying. They cost money.";
            if(!merchandiseKnown)welcome+=" Need a drink or a decent blade, ask me.";
            if(!boardKnown)welcome+=" Work comes through here, too. We can talk about that once I know you can come back.";
            return welcome;
        }
        public void Refuse(){offerRefused=true;refusals++;}
        public void Accept(){offerRefused=false;}
    }
}

