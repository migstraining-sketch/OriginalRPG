using System;
using System.Collections.Generic;

namespace WoodlandSpine
{
    // Small contextual node graph; predicates expose branches from explicit story flags.
    public sealed class DialogueChoice
    {
        public string label;
        public Action choose;
        public Func<bool> visible;
        public DialogueChoice(string label, Action choose, Func<bool> visible=null){this.label=label;this.choose=choose;this.visible=visible;}
    }
    public sealed class DialogueSession
    {
        public string speaker, text;
        // A continuation advances another speaker's exchange; it is never presented as player speech.
        public Action continueAction;
        public readonly List<DialogueChoice> choices=new List<DialogueChoice>();
        public DialogueSession(string speaker,string text,params DialogueChoice[] choices){this.speaker=speaker;this.text=text;this.choices.AddRange(choices);}
    }
}
