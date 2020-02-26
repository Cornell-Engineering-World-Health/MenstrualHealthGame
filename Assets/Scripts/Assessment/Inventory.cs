using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Controls what goes inside "inventory".
public class Inventory : MonoBehaviour, IHasChanged
{
	[SerializeField] Transform slots;
	[SerializeField] Text inventoryText;

	// Start is called before the first frame update
	void Start()
	{
        // Called everytime inventory is loaded.
		HasChanged();
	}

	public void HasChanged()
	{
		System.Text.StringBuilder builder = new System.Text.StringBuilder();
		builder.Append(" - ");
        // Loop through each transform
		foreach (Transform slotTransform in slots)
		{
            // Get gameObject if there is one
			GameObject item = slotTransform.GetComponent<Slot>().item;
            // If not null, then append that gameObj name to the string.
			if (item)
			{
				builder.Append(item.name);
				builder.Append(" - ");
			}
		}
		inventoryText.text = builder.ToString();
	}

}

// An interface which informs the inventory when something HasChanged.
// Add to EventSystems namespace, not necessary but good practice
namespace UnityEngine.EventSystems
{
    // MUST inherit from IEventSystemHandler to use EventSystem
	public interface IHasChanged : IEventSystemHandler
	{
		void HasChanged();
	}
}
