using UnityEngine;
class ButtonInteract : MonoBehaviour
    {
        public int buttonID;
        public ButtonSequencePuzzle puzzle;
        public MeshRenderer buttonRenderer;
        public Material defaultMaterial;
        private Material highlightMaterial;

        void Start()
        {
            // Create a highlight material dynamically
            highlightMaterial = new Material(defaultMaterial);
            highlightMaterial.color = puzzle.highlightColor;
        }
        void OnMouseDown()
        {
            if (puzzle != null)
            {
                puzzle.PressButton(buttonID);
            }
        }
        void OnMouseEnter()
        {
            if (buttonRenderer != null)
            {
                buttonRenderer.material = highlightMaterial; // Change color on hover
            }
        }

        void OnMouseExit()
        {
            if (buttonRenderer != null)
            {
                buttonRenderer.material = defaultMaterial; // Reset color when not hovered
            }
        }
    }
