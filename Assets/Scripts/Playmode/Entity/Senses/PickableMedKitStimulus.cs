using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class PickableMedKitStimulus : MonoBehaviour
    {
	    private PickableMedKit medKit;
        		
        		private void Awake()
        		{
        			InitializeComponent();
        		}
        
        		private void InitializeComponent()
        		{
        			medKit= transform.GetComponent<PickableMedKit>();
        		}
        
        		private void OnTriggerEnter2D(Collider2D other)
        		{
        			other.GetComponent<PickableMedKitSensor>()?.See(medKit);
        		}
        
        		private void OnTriggerExit2D(Collider2D other)
        		{
        			other.GetComponent<PickableMedKitSensor>()?.LooseSightOf(medKit);
        		}
    }
}