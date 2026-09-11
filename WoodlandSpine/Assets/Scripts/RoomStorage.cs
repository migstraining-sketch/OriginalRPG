using System;
using System.Collections.Generic;
using UnityEngine;

namespace WoodlandSpine
{
    public sealed class InventoryEntry
    {
        public string name;public WeaponData weapon;public BodyData body;
        public Func<Inventory,int> count;public Action<Inventory,int> set;
        public int Count(Inventory bag)=>weapon!=null?(bag.weapons.Contains(weapon)?1:0):body!=null?(bag.bodies.Contains(body)?1:0):count(bag);
    }
    public static class InventoryItems
    {
        public static List<InventoryEntry> List(Inventory bag)
        {
            var items=new List<InventoryEntry>();
            foreach(var w in bag.weapons)items.Add(new InventoryEntry{name=w.title,weapon=w});
            foreach(var b in bag.bodies)items.Add(new InventoryEntry{name=b.title,body=b});
            Add(items,"Bandage",b=>b.bandages,(b,n)=>b.bandages=n);
            Add(items,"Bloodleaf",b=>b.bloodleaf,(b,n)=>b.bloodleaf=n);
            Add(items,"Silvermoss",b=>b.silvermoss,(b,n)=>b.silvermoss=n);
            Add(items,"Mooncalf Milk",b=>b.mooncalfMilk,(b,n)=>b.mooncalfMilk=n);
            Add(items,"Clean field flask",b=>b.cleanFieldFlask?1:0,(b,n)=>b.cleanFieldFlask=n>0);
            Add(items,"Experimental potion",b=>b.experimentalPotion,(b,n)=>b.experimentalPotion=n);
            Add(items,"Health Potion",b=>b.healthPotions,(b,n)=>b.healthPotions=n);
            foreach(var food in bag.provisions.Keys){string key=food;Add(items,key,b=>b.provisions.TryGetValue(key,out int n)?n:0,(b,n)=>b.provisions[key]=n);}
            items.RemoveAll(i=>i.Count(bag)<=0);return items;
        }
        static void Add(List<InventoryEntry> list,string name,Func<Inventory,int> get,Action<Inventory,int> set)=>list.Add(new InventoryEntry{name=name,count=get,set=set});
        public static bool Transfer(Inventory from,Inventory to,InventoryEntry item,int quantity)
        {
            if(from==to||quantity<=0||item.Count(from)<quantity)return false;
            if(item.weapon!=null)
            {if(to.weapons.Contains(item.weapon))return false;from.weapons.Remove(item.weapon);if(from.weapon==item.weapon)from.weapon=null;to.Store(item.weapon);}
            else if(item.body!=null)
            {if(to.bodies.Contains(item.body))return false;from.bodies.Remove(item.body);if(from.body==item.body)from.body=null;to.Store(item.body);}
            else{int available=item.Count(from);item.set(from,available-quantity);item.set(to,item.Count(to)+quantity);}
            return true;
        }
    }
    public sealed class RoomStorage
    {
        public readonly Inventory chest=new Inventory();
        public bool visible;
        public InventoryEntry pending;
        Inventory source,destination;
        int quantity=1;
        Vector2 leftScroll,rightScroll;
        public void Cancel(){if(pending!=null)pending=null;else visible=false;}
        public void Draw(Inventory bag,float width,float height)
        {
            GUILayout.BeginArea(new Rect(50,100,width-100,height-140),GUI.skin.box);
            GUILayout.Label("Your room chest • stored items remain here when you travel");
            GUILayout.BeginHorizontal();Pane(bag,chest,"Inventory",ref leftScroll);Pane(chest,bag,"Room storage",ref rightScroll);GUILayout.EndHorizontal();
            if(pending!=null)
            {
                GUILayout.Label("Move "+pending.name+" ×"+quantity);
                quantity=Mathf.RoundToInt(GUILayout.HorizontalSlider(quantity,1,Mathf.Max(1,pending.Count(source))));
                if(GUILayout.Button("Transfer selected quantity")){InventoryItems.Transfer(source,destination,pending,quantity);pending=null;}
                if(GUILayout.Button("Cancel quantity [Esc]"))pending=null;
            }
            else if(GUILayout.Button("Close chest [Esc]"))visible=false;
            GUILayout.EndArea();
        }
        void Pane(Inventory from,Inventory to,string title,ref Vector2 scroll)
        {
            GUILayout.BeginVertical(GUI.skin.box,GUILayout.MinWidth(350));GUILayout.Label(title);scroll=GUILayout.BeginScrollView(scroll);
            foreach(var item in InventoryItems.List(from))
            {
                int count=item.Count(from);string equipped=item.weapon!=null&&from.weapon==item.weapon||item.body!=null&&from.body==item.body?" (unequip and store)":"";
                GUILayout.BeginHorizontal();GUILayout.Label(item.name+" ×"+count);
                if(GUILayout.Button("Move"+equipped,GUILayout.Width(190)))InventoryItems.Transfer(from,to,item,1);
                if(count>1&&GUILayout.Button("Quantity…",GUILayout.Width(110))){pending=item;source=from;destination=to;quantity=1;}
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();GUILayout.EndVertical();
        }
    }
}
