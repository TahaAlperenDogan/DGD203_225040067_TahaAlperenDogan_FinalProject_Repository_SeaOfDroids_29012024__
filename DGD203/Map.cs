using System;
using System.Numerics;
using System.ComponentModel;
using System.Diagnostics;
using DGD203_2;

public class Map
{
	private Game _theGame;

	private Vector2 _coordinates;

	private int[] _widthBoundaries;
	private int[] _heightBoundaries;

	private Location[] _locations;


	public Map(Game game, int width, int height)
	{
		_theGame = game;

		// Setting the width boundaries
		int widthBoundary = (width - 1) / 2;

        _widthBoundaries = new int[2];
        _widthBoundaries[0] = -widthBoundary;
		_widthBoundaries[1] = widthBoundary;

		// Setting the height boundaries
        int heightBoundary = (height - 1) / 2;

        _heightBoundaries = new int[2];
		_heightBoundaries[0] = -heightBoundary;
		_heightBoundaries[1] = heightBoundary;

		// Setting starting coordinates
        _coordinates = new Vector2(2,2);

		GenerateLocations();

		// Display result message
		Console.WriteLine($"\r\n░██████╗███████╗░█████╗░  ░█████╗░███████╗  ██████╗░██████╗░░█████╗░██╗██████╗░░██████╗\r\n██╔════╝██╔════╝██╔══██╗  ██╔══██╗██╔════╝  ██╔══██╗██╔══██╗██╔══██╗██║██╔══██╗██╔════╝\r\n╚█████╗░█████╗░░███████║  ██║░░██║█████╗░░  ██║░░██║██████╔╝██║░░██║██║██║░░██║╚█████╗░\r\n░╚═══██╗██╔══╝░░██╔══██║  ██║░░██║██╔══╝░░  ██║░░██║██╔══██╗██║░░██║██║██║░░██║░╚═══██╗\r\n██████╔╝███████╗██║░░██║  ╚█████╔╝██║░░░░░  ██████╔╝██║░░██║╚█████╔╝██║██████╔╝██████╔╝\r\n╚═════╝░╚══════╝╚═╝░░╚═╝  ░╚════╝░╚═╝░░░░░  ╚═════╝░╚═╝░░╚═╝░╚════╝░╚═╝╚═════╝░╚═════╝░");
        Console.WriteLine("PLAY IN FULLSCREEN FOR BETTER EXPERIENCE");
    }

    #region Coordinates

    public Vector2 GetCoordinates()
	{
		return _coordinates;
	}

	public void SetCoordinates(Vector2 newCoordinates)
	{
		_coordinates = newCoordinates;
	}

	#endregion

	#region Movement

	public void MovePlayer(int x, int y)
	{
		int newXCoordinate = (int)_coordinates[0] + x;
        int newYCoordinate = (int)_coordinates[1] + y;

		if (!CanMoveTo(newXCoordinate, newYCoordinate)) 
		{
            Console.WriteLine("Even if you try frame perfect you cant go beyond the DATAWALL");
            return;
        }

		_coordinates[0] = newXCoordinate;
		_coordinates[1] = newYCoordinate;

		CheckForLocation(_coordinates);
    }

	private bool CanMoveTo(int x, int y)
	{
		return !(x < _widthBoundaries[0] || x > _widthBoundaries[1] || y < _heightBoundaries[0] || y > _heightBoundaries[1]);
	}

    #endregion

    #region Locations

    private void GenerateLocations()
    {
        _locations = new Location[6];

        Vector2 neonCityLocation = new Vector2(0, 0);
        List<Item> neonCityItems = new List<Item>();
        neonCityItems.Add(Item.SickCoin);
        Location neonCity = new Location("Neon", LocationType.City, neonCityLocation, neonCityItems);
        _locations[0] = neonCity;

        Vector2 techHubLocation = new Vector2(-2, 2);
        List<Item> techHubItems = new List<Item>();
        techHubItems.Add(Item.pyCharm);
        Location techHub = new Location("Tech Hub", LocationType.City, techHubLocation, techHubItems);
        _locations[1] = techHub;

        Vector2 cyberDistrictLocation = new Vector2(1, -2);
        List<Item> cyberDistrictItems = new List<Item>();
        cyberDistrictItems.Add(Item.RunedomAccessMemory);
        Location cyberDistrict = new Location("Cyber District", LocationType.City, cyberDistrictLocation, cyberDistrictItems);
        _locations[2] = cyberDistrict;

        Vector2 neuralPlazaLocation = new Vector2(-2, -2);
        Location neuralPlaza = new Location("Neural Plaza,Fınally! You reached to your goal!\r\nYou can say settle and live in pice untill your battery gets empty\r\n░░░░░░░░░░░░░░░████░░░░░░░░░░░░░░░░░░░████\r\n░░░████░░░░░░░███████░░░░░░░░████░░░░░░░░░\r\n░░░████░░░░░░░░░░░░░░░░░░░████████░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░█████░░░░░░░██░██░░░░░░░░\r\n░██░░░░░░░░░███░░████░██░░░░░████░█░░░░░░░\r\n░███░░░░░░░█░██░██████░░█░░██████░█░███░░░\r\n░██░███████░░██░███████░██████████████░█░░\r\n░██░░░░░░░░░░██░██████████████████████████\r\n██████████████████████████████████████████\r\n██████████████████████████████████████████\r\n░░░░░█████████████████████████████████░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░████████████░░░\r\n░░░░░░░░░░░░░░███████░░░░░░░░░░░░░░░░░░███\r\n░░████████████░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░█████████████\r\n░░░░░░░░░░░░░░░░░░░░█████████░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\r\n", LocationType.City, neuralPlazaLocation);
        _locations[3] = neuralPlaza;

        Vector2 virtualArenaLocation = new Vector2(-2, 1);
        Location virtualArena = new Location("Virtual Arena", LocationType.Combat, virtualArenaLocation);
        _locations[4] = virtualArena;

        Vector2 dataFortressLocation = new Vector2(-1, -1);
        Location dataFortress = new Location("Data Fortress", LocationType.Combat, dataFortressLocation);
        _locations[5] = dataFortress;
    }

    public void CheckForLocation(Vector2 coordinates)
	{
        Console.WriteLine($"Your Zhip's location is DATAworldCoordinate: {_coordinates[0]},{_coordinates[1]}");

        if (IsOnLocation(_coordinates, out Location location))
        {
            if (location.Type == LocationType.Combat)
			{
				if (location.CombatAlreadyHappened) return;

				Console.WriteLine("ALERT!! MALWARE");
				Combat combat = new Combat(_theGame, location);

				combat.StartCombat();

			} else
			{
				Console.WriteLine($"You reached the {location.Name} {location.Type} Island");

				if (HasItem(location))
				{
					Console.WriteLine($"You found a tresure chest here!");
				}
			}
        }
    }

	private bool IsOnLocation(Vector2 coords, out Location foundLocation)
	{
		for (int i = 0; i < _locations.Length; i++)
		{
			if (_locations[i].Coordinates == coords)
			{
				foundLocation = _locations[i];
				return true;
			}
		}

		foundLocation = null;
		return false;
	}

	private bool HasItem(Location location)
	{
		return location.ItemsOnLocation.Count != 0;

		// ---- THE LONG FORM ----
		//if (location.ItemsOnLocation.Count == 0)
		//{
		//	return false;
		//} else
		//{
		//	return true;
		//}
	}

	public void TakeItem(Location location)
	{

	}

	public void TakeItem(Player player, Vector2 coordinates)
	{
		if (IsOnLocation(coordinates, out Location location))
		{
			if (HasItem(location))
			{
				Item itemOnLocation = location.ItemsOnLocation[0];

				player.TakeItem(itemOnLocation);
				location.RemoveItem(itemOnLocation);

				Console.WriteLine($"You found {itemOnLocation} in chest");

				return;
			}
		}

		Console.WriteLine("You looked at everywhere but there is no tresure!");
	}

	public void RemoveItemFromLocation(Item item)
	{
		for (int i = 0; i < _locations.Length; i++)
		{
			if (_locations[i].ItemsOnLocation.Contains(item))
			{
				_locations[i].RemoveItem(item);
			}
		}
	}

	#endregion
}