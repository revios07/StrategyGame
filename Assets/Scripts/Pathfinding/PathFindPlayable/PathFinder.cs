using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Toolbox;

public class PathFinder : MonoBehaviour
{
    private bool _isMoveEnded;
    [HideInInspector]
    public Vector3 startPos, endPos;
    public Tilemap tileMap;
    private List<Vector3> wayPoints;
    WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    public void FindPos(Vector3 targetPosition)
    {
        startPos = transform.position;
        endPos = targetPosition;

        wayPoints = AStar.FindPathClosest(tileMap, startPos, endPos);

        if(wayPoints != null)
        {
            MoveToPositions(wayPoints.ToArray());
        }
    }

    public void MoveToPositions(Vector3[] movePosses)
    {
        if (!_isMoveEnded)
            return;

        _isMoveEnded = false;
        StartCoroutine(MoveToLocation(movePosses));
    }

    private IEnumerator MoveToLocation(Vector3[] movePosition)
    {
        for (int i = 0; i < movePosition.Length; ++i)
        {
            while (true)
            {
                yield return waitForFixedUpdate;

                transform.position = Vector3.MoveTowards(transform.position, movePosition[i], 10f * Time.fixedDeltaTime);

                if (Vector3.Distance(transform.position, movePosition[i]) < 1f)
                {
                    break;
                }
            }
        }

        _isMoveEnded = true;
        Debug.Log("Move to position complete!");
    }
}
