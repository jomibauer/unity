using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LayoutAnchor))]
public class UIPanel : MonoBehaviour
{
    [Serializable]
    public class Position
    {
        public string name;
        public TextAnchor myAnchor;
        public TextAnchor parentAnchor;
        public Vector2 offset;

        public Position (string name)
        {
            this.name = name;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor): this(name)
        {
            this.myAnchor = myAnchor;
            this.parentAnchor = parentAnchor;
        }

        public Position(string name, TextAnchor myAnchor, TextAnchor parentAnchor, Vector2 offset) : this(name, myAnchor, parentAnchor)
        {
            this.offset = offset;
        }
    }

    //Serialize the position list so we can add positions via the inspector. 
    //Then, in Awake(), it gets turned into a dictionary map that will let us refer to the position by string.
    [SerializeField] List<Position> positionList;
    Dictionary<string, Position> positionMap;
    LayoutAnchor anchor;
    public Position CurrentPosition { get; private set; }
    public Tweener Transition { get; private set; }
    public bool InTransition { get { return Transition != null; }}

    void Awake()
    {
        anchor = GetComponent<LayoutAnchor>();
        positionMap = new Dictionary<string, Position>(positionList.Count);
        for(int i = positionList.Count -1; i>=0; --i)
        {
            AddPosition(positionList[i]);
        }
    }
    //get positions in the position map by indexing the panel instance directly
    public Position this[string name]
    {
        get
        {
            Position pos;
            positionMap.TryGetValue(name, out pos);
            return pos;
        }
    }
    public void AddPosition(Position pos)
    {
        positionMap[pos.name] = pos;
    }

    public void RemovePosition(Position pos)
    {
        if (positionMap.ContainsKey(pos.name))
        {
            positionMap.Remove(pos.name);
        }
    }

    public Tweener SetPosition (string positionName, bool animated)
    {
      return SetPosition(this[positionName], animated);
    }
    public Tweener SetPosition (Position p, bool animated)
    {
      CurrentPosition = p;
      if (CurrentPosition == null){ return null; }

      if (InTransition)
      {
        Transition.easingControl.Stop();
      }
        
      if (animated)
      {
        Transition = anchor.MoveToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
        return Transition;
      }
      else
      {
        anchor.SnapToAnchorPosition(p.myAnchor, p.parentAnchor, p.offset);
        return null;
      }
    }

    void Start ()
    {
      if (CurrentPosition == null && positionList.Count > 0)
      {
        SetPosition(positionList[0], false);
      }
        
    }
}
