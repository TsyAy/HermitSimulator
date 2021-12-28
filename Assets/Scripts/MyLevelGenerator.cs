using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLevelGenerator : MonoBehaviour
{   
    //Решил создать свой способ генерации мира, основан на шуме перлина и генерации как в Nuclear Throne(https://youtu.be/I74I_MhZIK8)
    //Проьлема в оптимизации, при способе передвижении через RB2d.velocity скорость нестабильна.
    //Думаю когда-нибудь доделаю игру на основе этой генерации
    //Делал ее 2 дня.

    //Надо добавить генерациб камней, веток, травы

    //поверхность
    enum gridSpace{field, forest, mountain, water};
    gridSpace[,] grid;
    //размеры поверхность
    [SerializeField] private Vector2 _roomSizeWorldUnits;
    [SerializeField] private float _scale= 1.0f;//cид мира 
    private float _worldUnitsPerOneGridCell = 1;
    private int _roomHeight, _roomWidth;
    private float xCoord,yCoord;
    //Префабы
    [SerializeField] private GameObject _forest, _field, _mountain, _water, _three;

    void Start()
    {
        _scale = Random.Range(3.0f, 5.0f);
        LevelCalculator();
        LevelSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LevelCalculator()
    {
        //рассчет шагов для шума перлина
        _roomHeight = Mathf.RoundToInt(_roomSizeWorldUnits.x/_worldUnitsPerOneGridCell);
        _roomWidth = Mathf.RoundToInt(_roomSizeWorldUnits.y/_worldUnitsPerOneGridCell);

        grid = new gridSpace[_roomWidth,_roomHeight];
        //Шумим
        for(int x=0;x<_roomWidth;x++)
        {
            for(int y=0;y<_roomHeight;y++)
            {
                xCoord = (float)x/_roomWidth*_scale;
                yCoord = (float)y/_roomHeight*_scale;

                float sample = Mathf.PerlinNoise(xCoord, yCoord);

                if(sample>=0 && sample<=0.25f)grid[x,y] = gridSpace.water;
                if(sample>0.25f && sample<=0.50f)grid[x,y] = gridSpace.field;
                if(sample>0.50f && sample<=0.75f)grid[x,y] = gridSpace.forest;
                if(sample>0.75f && sample<=1f)grid[x,y] = gridSpace.mountain;
            }
        }
    }

    private void LevelSpawn()
    {
        for(int x=0;x<_roomHeight;x++)
        {
            for(int y=0;y<_roomWidth;y++)
            {
                switch(grid[x,y])
                {
                    case gridSpace.water:
                        Spawn(x,y,_water);
                        break;
                    case gridSpace.field:
                        Spawn(x,y,_field);
                        break;
                    case gridSpace.forest:
                        Spawn(x,y,_field);
                        int threeSpawnLimit = Random.Range(1,5);
                        for (int t=0; t<threeSpawnLimit;t++)
                        {
                            SpawnObjects(x, y, _three);
                        }
                        break;
                    case gridSpace.mountain:
                        Spawn(x,y,_mountain);
                        break;
                }
            }
        }
    }

    private void Spawn(float x, float y, GameObject toSpawn)
    {
        Vector2 offset = _roomSizeWorldUnits/2.0f;
        Vector2 spawnPosition = new Vector2(x,y)*_worldUnitsPerOneGridCell-offset;

        Instantiate(toSpawn, spawnPosition, Quaternion.identity);
    }

    private void SpawnObjects(float x, float y, GameObject toSpawn)
    {
        Vector3 offset = _roomSizeWorldUnits/2.0f;
        Vector3 spawnPosition = new Vector3(x+Random.Range(-0.5f, 0.5f), y + Random.Range(-0.5f, 0.5f),-0.3f)*_worldUnitsPerOneGridCell-offset;
        Instantiate(toSpawn, spawnPosition, Quaternion.identity); 
    }
}
