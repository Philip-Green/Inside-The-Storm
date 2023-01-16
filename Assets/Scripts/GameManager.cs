using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum TurnStates { FirstPlayerTurn = 0, SecondPlayerTurn = 1 };
    public TurnStates turn;
    
    public static GameManager Instance;

    public TextMeshProUGUI gameText; 
    public GameObject treasure;
    public Player playerOne ;
    public Player playerTwo;
    public Platform[] platforms;

    private static Platform[,] _platformSpots = new Platform[7,7];
    private Platform _currentlySelectedPlatform;
    private Camera _cachedCamera;
    
    private enum MoveDirections { Up, Down, Left, Right}

    void Awake()
    {
        /*
         * Null reference may be cause by the instance being instantiated before the first frame.
         * Prior to frame 1, the game manager script has nothing in it so everything is null
         *
         * Upon further thinking, I will probably need to get an instance of the gameManager script whenever the
         * platforms change
        */
        
        Instance = this;
    }
    
    void Start()
    {
        gameText.text = "Mamdaou here";
        gameText.color = Color.blue;
        
        playerOne.SetPlayerPosition(0, 6);
        playerTwo.SetPlayerPosition(6, 0);
        treasure.transform.localPosition = new Vector3(18, 0, 18);
        
        // Manually assign each starting platform!
        // Bottom left
        MovePlatformToSpot(platforms[0], 0, 0);
        MovePlatformToSpot(platforms[1], 1, 0);
        MovePlatformToSpot(platforms[2], 0, 1);
        
        // Bottom right
        MovePlatformToSpot(platforms[3], 6, 0);
        MovePlatformToSpot(platforms[4], 6, 1);
        MovePlatformToSpot(platforms[5], 5, 0);
        
        // Top right
        MovePlatformToSpot(platforms[6], 6, 6);
        MovePlatformToSpot(platforms[7], 6, 5);
        MovePlatformToSpot(platforms[8], 5, 6);
        
        // Top left
        MovePlatformToSpot(platforms[9], 0, 6);
        MovePlatformToSpot(platforms[10], 1, 6);
        MovePlatformToSpot(platforms[11], 0, 5);
        
        // Cache the main cam
        _cachedCamera = Camera.main;
        
        // Set the instance of the gameobject to this
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (turn == TurnStates.FirstPlayerTurn)
            {
                turn = TurnStates.SecondPlayerTurn;
            }
            else
            {
                turn = TurnStates.FirstPlayerTurn;
            }
        }

        if (turn == TurnStates.FirstPlayerTurn)
        {
            gameText.color = Color.red;
        }
        else
        {
            gameText.color = Color.blue;
        }
        // Clicking!
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = _cachedCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast (ray, out hit))
            {
                if(hit.transform.CompareTag("Platform"))
                {
                    _currentlySelectedPlatform = hit.transform.gameObject.GetComponent<Platform>();
                    
                    //Debug.Log ("Selected platform at coords " + 
                    //           _currentlySelectedPlatform.GetX() + " / " + _currentlySelectedPlatform.GetY());
                }
                else {
                    Debug.Log ("This isn't a Platform");                
                }
            }

            // Every click, recolor
            ColorOnlySelectedPlatform();
        }
        
        // WASD!  Only if something's selected.
        if (_currentlySelectedPlatform != null) {
            if (Input.GetKeyDown(KeyCode.W)) {
                AttemptMoveInDirection(MoveDirections.Up);
            } else if (Input.GetKeyDown(KeyCode.S)) {
                AttemptMoveInDirection(MoveDirections.Down);
            } else if (Input.GetKeyDown(KeyCode.A)) {
                AttemptMoveInDirection(MoveDirections.Left);
            } else if (Input.GetKeyDown(KeyCode.D)) {
                AttemptMoveInDirection(MoveDirections.Right);
            }
        }
    }

    private void ColorOnlySelectedPlatform()
    {
        // Go through all your platforms, and change them to white
        foreach (Platform p in platforms)
        {
            p.GetComponent<MeshRenderer>().material.color = Color.white;
        }
        
        // Make the selected one yellow!
        if (_currentlySelectedPlatform != null)
        {
            _currentlySelectedPlatform.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
    }

    private void AttemptMoveInDirection(MoveDirections dir)
    {
        //Debug.Log("Attempting to move in direction " + dir);
        
        // We've got a cube and a direction.  Move it!
        int currX = _currentlySelectedPlatform.GetX();
        int currY = _currentlySelectedPlatform.GetY();

        // Remember: 0,0 is bottom-left.
        
        // From your current position, iterate through the grid to see if you can move.
        // For each spot - is it empty?  If so, move there.  If not, break.
        // TODO: Check for "treasure" in addition to null check to do win condition here.
        switch (dir)
        {
            case MoveDirections.Up:
                for (int j = currY + 1; j < 7; j++) {
                    if (_platformSpots[currX, j] == null) {
                        MovePlatformToSpot(_currentlySelectedPlatform, currX, j);
                    } else {
                        break;
                    }
                }
                break;
            case MoveDirections.Down:
                for (int j = currY - 1; j >= 0; j--) {
                    if (_platformSpots[currX, j] == null) {
                        MovePlatformToSpot(_currentlySelectedPlatform, currX, j);
                    } else {
                        break;
                    }
                }
                break;
            case MoveDirections.Left:
                for (int i = currX - 1; i >= 0; i--) {
                    if (_platformSpots[i, currY] == null) {
                        MovePlatformToSpot(_currentlySelectedPlatform, i, currY);
                    } else {
                        break;
                    }
                }
                break;
            case MoveDirections.Right:
                for (int i = currX + 1; i < 7; i++) {
                    if (_platformSpots[i, currY] == null) {
                        MovePlatformToSpot(_currentlySelectedPlatform, i, currY);
                    } else {
                        break;
                    }
                }
                break;
        }
        
        // Unselect afterwards.
        _currentlySelectedPlatform = null;
        ColorOnlySelectedPlatform();
    }

    private void MovePlatformToSpot(Platform platform, int x, int y)
    {
        // Get actual position by multiplying by 6!
        platform.transform.localPosition = new Vector3(x * 6, 0, y * 6);
        
        // Clear the old platform spot, if this is an already placed piece.
        if (platform.GetHasBeenPlaced()) {
            _platformSpots[platform.GetX(), platform.GetY()] = null;
        }
        
        // Set the new platform spot.
        _platformSpots[x, y] = platform;
        
        // Tell the platform where you're going, too.
        platform.SetCoordinates(x, y);
        
        //Debug.Log("Moving platform to spot " + x + " / " + y);
    }

    public Platform GetPlatformAt(int x, int y)
    {
        return _platformSpots[x, y];
    }
}
