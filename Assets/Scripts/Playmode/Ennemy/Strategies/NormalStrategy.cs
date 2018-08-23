using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Ennemy.BodyParts;
using Playmode.Movement;

namespace Playmode.Ennemy.Strategies
{

  public class NormalStrategy : IEnnemyStrategy
  {
    private readonly Mover mover;
    private readonly HandController handController;
    private Vector3 mouvement = new Vector3(100, 100, 0);

    public NormalStrategy(Mover mover , HandController handcontroller)
    {
      this.mover = mover;
      this.handController = handcontroller;
    }

    public void Act()
    {
      mover.Move(mouvement);
    }
    
  }
}