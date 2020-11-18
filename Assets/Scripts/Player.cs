using DefaultNamespace;
using Inputs;
using UnityEngine;

public class Player : MonoBehaviour {
    public IInputs inputs;
    //private Rigidbody rb = GameObject.Find("Player Ball").GetComponent<Rigidbody>();
    private Rigidbody rb;
    public Mover mover;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        mover = GetComponent<Mover>();
    }
    
    public void initializeConfigs(PlayerConfigs playerConfig) {
        inputs = playerConfig.getInputs();
    }

    public void shoot() {
        mover.shoot(rb);
    }
}
