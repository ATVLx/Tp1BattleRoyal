using Playmode.Ennemy;
using Playmode.Entity.Status;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableStar : Pickable
    {
        [SerializeField] private int durationInSeconds;


        protected override bool GetPicked(EnnemyController other)
        {
            //BEN_REVIEW : Personellement, j'aurais gérer le Timer d'invincibilité à partir du Pickable.
            //             J'aurais ajouté dans le GameObject un nouveau composant pour gérer l'invincibilité et qui
            //             s'autodétruirait après un certain temps.
            //
            //             Rien ne vous interdit d'ajouter des composants "At Runtime". C'est même une bonne idée
            //             quand vous avez une architecture à base de composants.
            other.transform.root.GetComponentInChildren<Health>().Invincibility(durationInSeconds);
            Destroy(this.gameObject);
            return true;
        }
    }
}