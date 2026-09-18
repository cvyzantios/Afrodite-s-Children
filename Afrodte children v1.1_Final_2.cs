using System;
using System.Collections.Generic;
using System.Threading;

// =========================================================
// WIND TURBINE CLASS
// =========================================================

class WindTurbine
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public double MaxPowerPerHour { get; set; } = 10.0;
    public int WindDynamics { get; set; }
    public double CurrentPower { get; set; }
}

// =========================================================
// FUSION REACTOR CLASS
// =========================================================

class FusionReactor
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public double PowerPerDay { get; set; } = 0.0;
    public bool Operational { get; set; } = true;
}

// =========================================================
// FIELD UNIT CLASS
// =========================================================

class FieldUnit
{
    public string Name { get; set; }
    public char Symbol { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Battery { get; set; } = 100;
    public int Condition { get; set; } = 100;
    public bool Available { get; set; } = true;
    public string Status { get; set; } = "BASE";

    public FieldUnit(string name, char symbol)
    {
        Name = name;
        Symbol = symbol;
    }
}

// =========================================================
// POINT CLASS
// =========================================================

class Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

// =========================================================
// A* NODE CLASS
// =========================================================

class Node
{
    public int X;
    public int Y;
    public Node Parent;
    public int G;
    public int H;

    public int F => G + H;

    public Node(int x, int y, Node parent, int g, int h)
    {
        X = x;
        Y = y;
        Parent = parent;
        G = g;
        H = h;
    }
}

// =========================================================
// HYDROPONIZED FARM CLASS
// =========================================================

class HydroponizedFarm
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public double ProductionPerCycleKg { get; set; } = 10.0;
    public double ProductionIntervalSeconds { get; set; } = 30.0;
    public DateTime LastProductionTime { get; set; } = DateTime.Now;
    public bool Operational { get; set; } = true;
}

// =========================================================
// FOOD WAREHOUSE CLASS
// =========================================================

class FoodWarehouse
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public double CurrentFoodTons { get; set; } = 0.0;
    public double MaximumCapacityTons { get; set; } = 30.0;
    public double ProductionPerCycleTons { get; set; } = 10.0;
    public double ProductionIntervalSeconds { get; set; } = 30.0;
    public double ConsumptionPerCycleTons { get; set; } = 2.0;
    public double ConsumptionIntervalSeconds { get; set; } = 15.0;
    public DateTime LastConsumptionTime { get; set; } = DateTime.Now;
    public bool Operational { get; set; } = true;
}

// =========================================================
// COLONY BUILDING CLASSES
// =========================================================

class HarpMachine
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public bool Operational { get; set; } = true;
}

class Spaceport
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public bool Operational { get; set; } = true;
}

class OxygenFactory
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Condition { get; set; } = 100;
    public double OxygenCubicMeters { get; set; } = 0.0;
    public double MaximumOxygen { get; set; } = 4000.0;
    public bool Operational { get; set; } = true;
}

// =========================================================
// MAIN PROGRAM
// =========================================================

class Program
{
    static void Main()
    {
        try
        {
            Random rand = new Random();

            ShowIntro();

            Console.WriteLine("In the year 2056, humanity attempted to colonize Mars.");
            Console.WriteLine("However, low gravity and scarce resources");
            Console.WriteLine("forced the operation to slow down.");
            Console.WriteLine("By 2100, it was decided to redirect efforts");
            Console.WriteLine("toward Venus.");
            Console.WriteLine("Giant zeppelin complexes were deployed");
            Console.WriteLine("to form the first aerial bases.");
            Console.WriteLine("Ten years later, the mandate was issued:");
            Console.WriteLine("Establish subsurface laboratories");
            Console.WriteLine("beneath the basalt crust.");
            Console.WriteLine("Secure the landing zone.");
            Console.WriteLine("Initiate the long-term terraforming process.");
            Console.WriteLine("The Council has chosen you as Commander");
            Console.WriteLine("to oversee the first phases of the operation.");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("      COMMANDER, MISSION CONTROL IS YOURS !!!!!");
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine();

            WaitForStart();

            char[,] grid = new char[30, 30];

            for (int row = 0; row < grid.GetLength(0); row++)
            {
                for (int column = 0; column < grid.GetLength(1); column++)
                {
                    grid[row, column] = '.';
                }
            }

            // =====================================================
            // TERRAIN
            // =====================================================

            grid[7, 8] = 'M'; grid[7, 9] = 'M'; grid[7, 10] = 'M';
            grid[8, 7] = 'M'; grid[8, 8] = 'M'; grid[8, 9] = 'M';
            grid[8, 10] = 'M'; grid[8, 11] = 'M';
            grid[9, 6] = 'M'; grid[9, 7] = 'M'; grid[9, 8] = 'M';
            grid[9, 9] = 'M'; grid[9, 10] = 'M'; grid[9, 11] = 'M';
            grid[9, 12] = 'M';
            grid[10, 7] = 'M'; grid[10, 8] = 'M'; grid[10, 9] = 'M';
            grid[10, 10] = 'M'; grid[10, 11] = 'M';
            grid[11, 8] = 'M'; grid[11, 9] = 'M'; grid[11, 10] = 'M';

            grid[12, 7] = 'R'; grid[12, 8] = 'R'; grid[12, 9] = 'R';
            grid[12, 10] = 'R'; grid[12, 11] = 'R';
            grid[13, 7] = 'R'; grid[13, 8] = 'R'; grid[13, 9] = 'R';
            grid[13, 10] = 'R'; grid[13, 11] = 'R';

            grid[14, 8] = 'P'; grid[14, 9] = 'P'; grid[14, 10] = 'P';
            grid[15, 8] = 'P'; grid[15, 9] = 'P'; grid[15, 10] = 'P';

            // =====================================================
            // LANDING PROTOCOL
            // =====================================================

            int attempts = 3;
            bool landingSuccessful = false;
            int shipX = 0;
            int shipY = 0;
            char shipTerrain = '.';

            while (attempts > 0 && !landingSuccessful)
            {
                Console.Clear();
                DrawGrid(grid, null);

                Console.WriteLine();
                Console.WriteLine("M = Mountain");
                Console.WriteLine("R = Rocky Foothills");
                Console.WriteLine("P = Plateau");
                Console.WriteLine("S = Spaceship");
                Console.WriteLine("r = Rover");
                Console.WriteLine("L = Laboratory");
                Console.WriteLine("A = Android");
                Console.WriteLine("B = Discovered Basalt");
                Console.WriteLine("W = Wind Turbine");
                Console.WriteLine(". = Normal Ground");

                Console.WriteLine();
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("COMMANDER - SELECT LANDING SITE");
                Console.WriteLine($"LANDING ATTEMPTS REMAINING: {attempts}");
                Console.WriteLine("----------------------------------------------");

                int x;

                while (true)
                {
                    Console.Write("X: ");

                    if (int.TryParse(Console.ReadLine(), out x))
                    {
                        if (x >= 0 && x < grid.GetLength(1))
                            break;
                    }

                    Console.WriteLine("Invalid X! Please enter a number between 0 and 29.");
                }

                int y;

                while (true)
                {
                    Console.Write("Y: ");

                    if (int.TryParse(Console.ReadLine(), out y))
                    {
                        if (y >= 0 && y < grid.GetLength(0))
                            break;
                    }

                    Console.WriteLine("Invalid Y! Please enter a number between 0 and 29.");
                }

                char terrain = grid[y, x];

                if (terrain == 'R' || terrain == 'P')
                {
                    landingSuccessful = true;
                    shipX = x;
                    shipY = y;
                    shipTerrain = terrain;
                    grid[y, x] = 'S';

                    Console.Clear();
                    DrawGrid(grid, null);

                    Console.WriteLine();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          LANDING SITE ACCEPTED!");
                    Console.WriteLine("==============================================");
                    Console.WriteLine();
                    Console.WriteLine($"Spaceship landed at X={x}, Y={y}");

                    string spaceScene = @"

#######################################
## Aphrodite's Children version 1.0  ##
#######################################
####################################### ## Afrodite's Childen version 1.1 ## #######################################

                                     .                                                                  .    .
                               .                                                                         .    .
                                     .                                                              .        .
                       .                                  .  _---_                                        .
                             .                         .-'         '-.        . . . . . .
                     .                               /     .-----.    \     . . . . . . . . .
              .---.                                 |    .'  . .  '.   |   . . . . . . . . . .
             /     \     .                          |   /   .   .   \  |   . . . . . . . . . .
            *       *                               |  |   .  ( )  . | |   . . . . . . . . . .
             \     /                                |   \   .   .   /  |   . . . . . . . . . .
              '---'                                  |    '.  . .  .'   |   . . . . . . . . .
                                                      \     '-----'    /     . . . . . . . .
                                                       '-.         .-'        . . . . . .
                                                          '-------'
                                                                                                .
                                             _..._                                                  .
                                          .-'     '-.                                             .
                                         /  .---.    \                                              .
                                        |  / AEGEAN \ |
                                        | |    1     ||
                                        |  \       /  |
                                         \  '---'    /
                                          '-._____.-'
                                              |$|
                                            ._| |________________
                                          / | | |                \ 
                                         /  |_|_|                 \ 
                                     .-'---------------------------'-.
                                    /   |  AEGEAN 1  |              \ 
                              .----/----|____________|_______________\----.
                             /    /   /                    |          \    \ 
                            |====|===|=====================|===========|====|
                            |    |   |  /|  /|  /|  /|     |  |   ()|  |    |
                            |    |   | / | / | / | / |     |  |     |  |    |
           _                |____|___|/__|/__|/__|/__|_____|__|_____|__|____|                _
         /   \                  | |  |                     |  |  | |                        /   \ 
        / /|\ \                 | |  |                     |  |  | |                       / /|\ \ 
       / / | \ \               / /    \                   /    \  \ \                     / / | \ \ 
      / /  |  \ \             / /      \                 /      \  \ \                   / /  |  \ \ 
     / /   |   \ \          _/_/        \_\             /_/        \_\_\                 / /   |   \ \ 
    / /    |    \ \        |___|        |___|          |___|        |___|               / /    |    \ \ 
===/=/=====|=====\=\==================================================================/=/=====|=====\=\===
  . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
 . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .
. . . . . . . . . . . . . . . . . . . . . . (  CRATER  ) . . . . . . . . . . . . . . . . . . . . . . . . . . .
 . . . . . . . . . . . . . . . . . . . . .   \_______/   . . . . . . . . . . . . . . . . . . . . . . . . . . .
  . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . .

==================================================
AEGEAN 1 HAS SUCCESSFULLY LANDED
==================================================
";

                    Console.WriteLine(spaceScene);
                    Console.WriteLine();
                    Console.WriteLine("Press any key to deploy the Rover...");
                    Console.ReadKey();
                }
                else
                {
                    attempts--;

                    Console.WriteLine();
                    Console.WriteLine("==============================================");
                    Console.WriteLine("          LANDING IMPOSSIBLE!");
                    Console.WriteLine("==============================================");

                    if (terrain == 'M')
                        Console.WriteLine("Reason: Mountain terrain.");
                    else
                        Console.WriteLine("Reason: Unsuitable terrain.");

                    Console.WriteLine();
                    Console.WriteLine($"Attempts remaining: {attempts}");

                    if (attempts > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Please select another landing site.");
                        Thread.Sleep(1500);
                    }
                }
            }

            if (!landingSuccessful)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.WriteLine();
                Console.WriteLine("          LANDING PROTOCOL FAILED");
                Console.WriteLine();
                Console.WriteLine("               MISSION ABORTED");
                Console.WriteLine();
                Console.WriteLine("==============================================");
                Console.ReadKey();
                return;
            }

            // =====================================================
            // ROVER DEPLOYMENT
            // =====================================================

            Console.Clear();
            DrawGrid(grid, null);
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("               ROVER DEPLOYMENT");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("Deploying Rover from Spaceship...");
            Console.WriteLine();
            Thread.Sleep(1500);

            int roverX = -1;
            int roverY = -1;

            int[,] roverOffsets = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

            for (int i = 0; i < roverOffsets.GetLength(0); i++)
            {
                int checkY = shipY + roverOffsets[i, 0];
                int checkX = shipX + roverOffsets[i, 1];

                if (IsInside(grid, checkX, checkY) && grid[checkY, checkX] != 'M')
                {
                    roverX = checkX;
                    roverY = checkY;
                    break;
                }
            }

            if (roverX == -1)
            {
                Console.WriteLine("ERROR: No suitable cell found for Rover deployment.");
                Console.ReadKey();
                return;
            }

            FieldUnit rover = new FieldUnit("ROVER-01", 'r');
            rover.X = roverX;
            rover.Y = roverY;
            rover.Status = "BASE";

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("               ROVER DEPLOYED");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine($"Rover position: X={roverX}, Y={roverY}");
            Console.WriteLine();
            Console.WriteLine("Rover is ready to begin surface survey.");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            // =====================================================
            // ROVER SEARCH
            // =====================================================

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("          ROVER SURVEY PROTOCOL");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("Rover is searching for a suitable");
            Console.WriteLine("rocky location near the landing site...");
            Console.WriteLine();
            Thread.Sleep(1500);

            int drillingX = -1;
            int drillingY = -1;
            int searchRadius = 3;

            for (int row = shipY - searchRadius; row <= shipY + searchRadius; row++)
            {
                for (int column = shipX - searchRadius; column <= shipX + searchRadius; column++)
                {
                    if (IsInside(grid, column, row))
                    {
                        if (grid[row, column] == 'R')
                        {
                            drillingX = column;
                            drillingY = row;
                            break;
                        }
                    }
                }

                if (drillingX != -1)
                    break;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("          ROVER SURVEY COMPLETE");
            Console.WriteLine("==============================================");
            Console.WriteLine();

            if (drillingX != -1)
            {
                Console.WriteLine("SUITABLE DRILLING SITE FOUND!");
                Console.WriteLine();
                Console.WriteLine($"Recommended location: X={drillingX}, Y={drillingY}");
                Console.WriteLine("Terrain: Rocky Foothills");
                Console.WriteLine();
                Console.WriteLine("This location is recommended");
                Console.WriteLine("for the future Laboratory.");
            }
            else
            {
                Console.WriteLine("NO SUITABLE DRILLING SITE FOUND");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to proceed with Laboratory setup...");
            Console.ReadKey();

            // =====================================================
            // LABORATORY
            // =====================================================

            if (drillingX == -1)
            {
                Console.WriteLine("Mission cannot continue without a laboratory site.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("      COMMANDER TRANSMISSION INCOMING");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("ROVER: Commander, drilling site is optimal.");
            Console.WriteLine("Initiating subsurface laboratory excavation.");
            Console.WriteLine("Deploying Autonomous Android Robots AR-01 and AR-02 for support.");
            Console.WriteLine();
            Thread.Sleep(1500);

            grid[drillingY, drillingX] = 'L';

            FieldUnit ar1 = new FieldUnit("AR-01", 'A');
            FieldUnit ar2 = new FieldUnit("AR-02", 'A');

            int robotsPlaced = 0;
            int[,] offsets = { { 1, 0 }, { -1, 0 }, { 0, 1 }, { 0, -1 } };

            for (int i = 0; i < offsets.GetLength(0); i++)
            {
                int checkY = drillingY + offsets[i, 0];
                int checkX = drillingX + offsets[i, 1];

                if (IsInside(grid, checkX, checkY))
                {
                    if (grid[checkY, checkX] != 'M' &&
                        grid[checkY, checkX] != 'S' &&
                        grid[checkY, checkX] != 'L')
                    {
                        robotsPlaced++;

                        if (robotsPlaced == 1)
                        {
                            ar1.X = checkX;
                            ar1.Y = checkY;
                        }
                        else if (robotsPlaced == 2)
                        {
                            ar2.X = checkX;
                            ar2.Y = checkY;
                        }

                        if (robotsPlaced == 2)
                            break;
                    }
                }
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover, ar1, ar2 });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("      LABORATORY EXCAVATION IN PROGRESS");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine($"Robots deployed near Lab site: {robotsPlaced}/2");
            Console.ReadKey();

            // =====================================================
            // LAB CONSTRUCTION
            // =====================================================

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover, ar1, ar2 });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("       CONSTRUCTION PHASE 2");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("Excavating basalt layers...");
            Thread.Sleep(1000);
            Console.WriteLine("Creating underground chambers...");
            Thread.Sleep(1000);
            Console.WriteLine("Installing life support systems...");
            Thread.Sleep(1000);
            Console.WriteLine("Installing communication systems...");
            Thread.Sleep(1000);
            Console.WriteLine("Power systems online...");
            Thread.Sleep(1000);
            Console.WriteLine("Environmental control systems online...");
            Thread.Sleep(1000);

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit> { rover, ar1, ar2 });
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("   LABORATORY CONSTRUCTION COMPLETE");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("Laboratory Status : OPERATIONAL");
            Console.WriteLine("Crew Capacity     : 7 Personnel");
            Console.WriteLine("Life Support      : ACTIVE");
            Console.WriteLine("Communications    : ACTIVE");
            Console.WriteLine();
            Console.WriteLine("All personnel have been transferred");
            Console.WriteLine("to the underground laboratory.");
            Console.WriteLine();
            Console.WriteLine("AEGEAN 1 has completed its mission.");
            Thread.Sleep(1500);
            Console.WriteLine();
            Console.WriteLine("AEGEAN 1 departing...");
            Thread.Sleep(1000);
            Console.WriteLine("Departure successful.");
            Console.WriteLine();
            Console.WriteLine("Commander, Aphrodite Base is now");
            Console.WriteLine("fully under your command.");
            Console.WriteLine();
            Console.WriteLine("==============================================");
            Console.WriteLine("          MISSION 01 COMPLETE");
            Console.WriteLine("==============================================");
            Console.WriteLine();
            Console.WriteLine("Press any key to initialize Mission 02 Tactical Command...");
            Console.ReadKey();

            grid[shipY, shipX] = shipTerrain;

            // =====================================================
            // BASE METRICS
            // =====================================================

            int basePower = 80;
            int foodRationsDays = 7;
            int lifeSupportDays = 6;
            int basaltMined = 0;

            List<WindTurbine> windTurbines = new List<WindTurbine>();
            List<FusionReactor> fusionReactors = new List<FusionReactor>();
            int currentDay = 1;

            int totalCrew = 7;
            int availableCrew = 7;
            int assignedCrew = 0;

            bool ar1Available = true;
            bool ar2Available = true;

            FieldUnit ar3 = null;
            FieldUnit ar4 = null;
            FieldUnit rover2 = null;

            FieldUnit ar5 = null;
            FieldUnit ar6 = null;
            FieldUnit ar7 = null;
            FieldUnit ar8 = null;
            FieldUnit ar9 = null;

            FieldUnit rover3 = null;
            FieldUnit rover4 = null;

            List<FieldUnit> colonyAndroids = new List<FieldUnit>();
            FieldUnit rover5 = null;
            FieldUnit rover6 = null;
            FieldUnit builder3D = null;

            bool resupplyRequested = false;
            int resupplyEtaDays = -1;

            int ar1Battery = 100;
            int ar2Battery = 100;
            int roverBattery = 100;

            int ar1Condition = 100;
            int ar2Condition = 100;
            int roverCondition = 100;

            bool basaltDiscovered = false;
            int basaltX = -1;
            int basaltY = -1;
            int basaltReserveTons = 0;

            int maxBasaltStorage = 150;

            // =====================================================
            // LAB EXTENSION REQUIREMENTS
            // =====================================================

            int requiredBasaltForExpansion = 120;
            int requiredTurbinesForExpansion = 3;

            bool laboratoryExpansionUnlocked = false;
            bool laboratoryExpanded = false;

            // =====================================================
            // BASE CREATION REQUIREMENTS
            // =====================================================

            int requiredBasaltForBaseCreation = 120;
            int requiredTurbinesForBaseCreation = 3;

            bool baseCreationUnlocked = false;
            bool baseCreated = false;

            // =====================================================
            // BASE EXTENSION REQUIREMENTS
            // =====================================================

            int requiredBasaltForBaseExpansion = 300;
            int requiredTurbinesForBaseExpansion = 6;
            int requiredHydroponizedForBaseExpansion = 200;
            int requiredFarmsForBaseExpansion = 2;
            int requiredWarehouseForBaseExpansion = 1;

            bool baseExpansionUnlocked = false;
            bool baseExpanded = false;

            // =====================================================
            // COLONY REQUIREMENTS
            // =====================================================

            int requiredTurbinesForColony = 12;
            int requiredBasaltForColony = 1200;
            int requiredHydroForColony = 800;
            int requiredFarmsForColony = 2;
            int requiredWarehouseForColony = 1;
            int requiredFoodTonsForColony = 10;

            bool colonyUnlocked = false;
            bool colonyExpanded = false;

            // =====================================================
            // COLONY EXTENSION REQUIREMENTS (NEW)
            // =====================================================

            int requiredTurbinesForColonyExtension = 17;
            int requiredBasaltForColonyExtension = 2000;
            int requiredHydroForColonyExtension = 1500;
            int requiredFoodTonsForColonyExtension = 25;
            bool requiredFusionForColonyExtension = true;
            bool requiredHarpForColonyExtension = true;
            bool requiredOxygenForColonyExtension = true;

            bool colonyExtensionUnlocked = false;
            bool colonyExtended = false;

            double hydroponizedProductionKg = 0;
            const double hydroponizedMaximumKg = 250.0;
            const double hydroponizedProductionSeconds = 180.0;
            bool hydroponizedProductionStarted = false;
            DateTime hydroponizedProductionStartTime = DateTime.MinValue;

            List<HydroponizedFarm> hydroponizedFarms = new List<HydroponizedFarm>();
            FoodWarehouse foodWarehouse = null;

            double foodConsumptionAccumulator = 0.0;

            HarpMachine harpMachine = null;
            Spaceport spaceport = null;
            OxygenFactory oxygenFactory = null;

            bool harpMachineBuilt = false;
            bool spaceportBuilt = false;
            bool oxygenFactoryBuilt = false;

            int requiredAdditionalTurbinesForFinalBase = 3;
            int requiredFarmsForFinalBase = 2;
            int requiredWarehouseForFinalBase = 1;
            int requiredFinalBasalt = 800;

            bool finalBaseObjectiveCompleted = false;

            int fieldTickMilliseconds = 500;

            bool missionActive = true;

            // =====================================================
            // MISSION 02 MAIN LOOP
            // =====================================================

            while (missionActive)
            {
                try
                {
                    UpdateHydroponizedProduction(
                        ref hydroponizedProductionKg,
                        hydroponizedProductionStarted,
                        hydroponizedProductionStartTime,
                        hydroponizedMaximumKg,
                        hydroponizedProductionSeconds);

                    UpdateBaseFoodSystem(hydroponizedFarms, foodWarehouse);

                    // =================================================
                    // CHECK LAB EXTENSION UNLOCK
                    // =================================================

                    if (!laboratoryExpanded &&
                        windTurbines.Count >= requiredTurbinesForExpansion &&
                        basaltMined >= requiredBasaltForExpansion)
                    {
                        laboratoryExpansionUnlocked = true;
                    }

                    // =================================================
                    // CHECK BASE CREATION UNLOCK
                    // =================================================

                    if (laboratoryExpanded &&
                        !baseCreated &&
                        windTurbines.Count >= requiredTurbinesForBaseCreation &&
                        basaltMined >= requiredBasaltForBaseCreation)
                    {
                        baseCreationUnlocked = true;
                    }

                    // =================================================
                    // CHECK BASE EXTENSION UNLOCK
                    // =================================================

                    if (baseCreated &&
                        !baseExpanded &&
                        basaltMined >= requiredBasaltForBaseExpansion &&
                        windTurbines.Count >= requiredTurbinesForBaseExpansion &&
                        hydroponizedProductionKg >= requiredHydroponizedForBaseExpansion &&
                        hydroponizedFarms.Count >= requiredFarmsForBaseExpansion &&
                        foodWarehouse != null)
                    {
                        baseExpansionUnlocked = true;
                    }

                    // =================================================
                    // CHECK COLONY UNLOCK
                    // =================================================

                    if (baseExpanded &&
                        !colonyExpanded &&
                        windTurbines.Count >= requiredTurbinesForColony &&
                        basaltMined >= requiredBasaltForColony &&
                        hydroponizedProductionKg >= requiredHydroForColony &&
                        hydroponizedFarms.Count >= requiredFarmsForColony &&
                        foodWarehouse != null &&
                        foodWarehouse.CurrentFoodTons >= requiredFoodTonsForColony)
                    {
                        colonyUnlocked = true;
                    }

                    // =================================================
                    // CHECK COLONY EXTENSION UNLOCK (NEW)
                    // =================================================

                    if (colonyExpanded &&
                        !colonyExtended &&
                        windTurbines.Count >= requiredTurbinesForColonyExtension &&
                        basaltMined >= requiredBasaltForColonyExtension &&
                        hydroponizedProductionKg >= requiredHydroForColonyExtension &&
                        foodWarehouse != null &&
                        foodWarehouse.CurrentFoodTons >= requiredFoodTonsForColonyExtension &&
                        fusionReactors.Count >= 1 &&
                        harpMachineBuilt &&
                        oxygenFactoryBuilt)
                    {
                        colonyExtensionUnlocked = true;
                    }

                    List<FieldUnit> allUnits = new List<FieldUnit>();

                    if (rover != null && rover.Status != "BASE" && rover.Status != "RETURNED") allUnits.Add(rover);
                    if (ar1 != null && ar1.Status != "BASE" && ar1.Status != "RETURNED") allUnits.Add(ar1);
                    if (ar2 != null && ar2.Status != "BASE" && ar2.Status != "RETURNED") allUnits.Add(ar2);

                    if (baseCreated && ar3 != null && ar3.Status != "BASE" && ar3.Status != "RETURNED") allUnits.Add(ar3);
                    if (baseCreated && ar4 != null && ar4.Status != "BASE" && ar4.Status != "RETURNED") allUnits.Add(ar4);
                    if (baseCreated && rover2 != null && rover2.Status != "BASE" && rover2.Status != "RETURNED") allUnits.Add(rover2);

                    if (baseExpanded && ar5 != null && ar5.Status != "BASE" && ar5.Status != "RETURNED") allUnits.Add(ar5);
                    if (baseExpanded && ar6 != null && ar6.Status != "BASE" && ar6.Status != "RETURNED") allUnits.Add(ar6);
                    if (baseExpanded && ar7 != null && ar7.Status != "BASE" && ar7.Status != "RETURNED") allUnits.Add(ar7);
                    if (baseExpanded && ar8 != null && ar8.Status != "BASE" && ar8.Status != "RETURNED") allUnits.Add(ar8);
                    if (baseExpanded && ar9 != null && ar9.Status != "BASE" && ar9.Status != "RETURNED") allUnits.Add(ar9);
                    if (baseExpanded && rover3 != null && rover3.Status != "BASE" && rover3.Status != "RETURNED") allUnits.Add(rover3);
                    if (baseExpanded && rover4 != null && rover4.Status != "BASE" && rover4.Status != "RETURNED") allUnits.Add(rover4);

                    if (colonyExpanded)
                    {
                        foreach (FieldUnit android in colonyAndroids)
                        {
                            if (android != null && android.Status != "BASE" && android.Status != "RETURNED")
                                allUnits.Add(android);
                        }
                        if (rover5 != null && rover5.Status != "BASE" && rover5.Status != "RETURNED") allUnits.Add(rover5);
                        if (builder3D != null && builder3D.Status != "BASE" && builder3D.Status != "RETURNED") allUnits.Add(builder3D);
                    }

                    if (colonyExtended && rover6 != null && rover6.Status != "BASE" && rover6.Status != "RETURNED") allUnits.Add(rover6);

                    Console.Clear();
                    DrawGrid(grid, allUnits);

                    Console.WriteLine();
                    Console.WriteLine("=================================================================================");
                    Console.WriteLine($"        APHRODITE BASE - COMMAND CENTER (MISSION 02) | DAY {currentDay}");
                    Console.WriteLine("=================================================================================");

                    if (foodRationsDays <= 3 || lifeSupportDays <= 3 || basePower <= 25)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("!!! WARNING: CRITICAL RESOURCE RESERVES LOW! CALL RESUPPLY IMMEDIATELY !!!");
                        Console.ResetColor();
                    }

                    // =================================================
                    // BASE TELEMETRY (LOGISTICS)
                    // =================================================

                    Console.WriteLine("[BASE TELEMETRY]");
                    Console.WriteLine($"- Base Power Grid       : {basePower}%");
                    Console.WriteLine($"- Life Support Reserve  : {lifeSupportDays} Days");
                    Console.WriteLine($"- Crew Food Rations     : {foodRationsDays} Days ({totalCrew} Personnel)");
                    Console.WriteLine($"- Basalt Raw Materials  : {basaltMined} / {maxBasaltStorage} Tons");
                    Console.WriteLine($"- Active Wind Turbines  : {windTurbines.Count} (Symbol: W)");
                    Console.WriteLine($"- Available Personnel   : {availableCrew}/{totalCrew}");

                    if (!baseExpanded)
                    {
                        Console.WriteLine($"- Hydroponized Production : {hydroponizedProductionKg:F0} / {hydroponizedMaximumKg:F0} kg");
                    }

                    Console.WriteLine();
                    Console.WriteLine("[FOOD PRODUCTION SYSTEM]");
                    Console.WriteLine($"- Hydroponized Farms    : {hydroponizedFarms.Count}/2");

                    if (foodWarehouse != null)
                    {
                        Console.WriteLine($"- Food Warehouse        : {foodWarehouse.CurrentFoodTons:F0} / {foodWarehouse.MaximumCapacityTons:F0} Tons");

                        if (foodWarehouse.CurrentFoodTons >= foodWarehouse.MaximumCapacityTons)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine("- WAREHOUSE STATUS      : FULL");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine("- Food Warehouse        : NOT CONSTRUCTED");
                    }

                    // =================================================
                    // POWER PRODUCTION (LOGISTICS)
                    // =================================================

                    double windPowerTotal = 0;
                    foreach (WindTurbine turbine in windTurbines)
                        windPowerTotal += turbine.CurrentPower;

                    double fusionPowerTotal = 0;
                    if (fusionReactors.Count > 0)
                    {
                        fusionPowerTotal = windTurbines.Count * 0.5;
                    }

                    Console.WriteLine();
                    Console.WriteLine("[POWER PRODUCTION]");
                    Console.WriteLine($"- Wind Turbines Output  : {windPowerTotal:F1}% / day");
                    Console.WriteLine($"- Fusion Reactor Output : +{fusionPowerTotal:F1}% / day");
                    Console.WriteLine($"- Total Production      : {windPowerTotal + fusionPowerTotal:F1}% / day");

                    if (basaltDiscovered)
                    {
                        Console.WriteLine($"- Basalt Deposit        : DISCOVERED at X={basaltX}, Y={basaltY}");
                        Console.WriteLine($" - Deposit Reserve      : {basaltReserveTons} Tons");
                    }
                    else
                    {
                        Console.WriteLine("- Basalt Deposit        : NOT YET DISCOVERED");
                    }

                    if (windTurbines.Count > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("[WIND TURBINE TELEMETRY]");

                        for (int i = 0; i < windTurbines.Count; i++)
                        {
                            WindTurbine turbine = windTurbines[i];
                            Console.WriteLine($"W-{i + 1:00} | X={turbine.X}, Y={turbine.Y} | Condition={turbine.Condition}% | Wind={turbine.WindDynamics}% | Output={turbine.CurrentPower:F1}/h");
                        }
                    }

                    if (fusionReactors.Count > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("[FUSION REACTOR TELEMETRY]");

                        for (int i = 0; i < fusionReactors.Count; i++)
                        {
                            FusionReactor reactor = fusionReactors[i];
                            Console.WriteLine($"R-{i + 1:00} | X={reactor.X}, Y={reactor.Y} | Condition={reactor.Condition}% | Output={reactor.PowerPerDay:F1}% / day");
                        }
                    }

                    if (hydroponizedFarms.Count > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine("[HYDROPONIZED FARM TELEMETRY]");

                        for (int i = 0; i < hydroponizedFarms.Count; i++)
                        {
                            HydroponizedFarm farm = hydroponizedFarms[i];
                            Console.WriteLine($"H-{i + 1:00} | X={farm.X}, Y={farm.Y} | Condition={farm.Condition}% | Output=+10 Tons / 30 sec");
                        }
                    }

                    if (colonyExpanded)
                    {
                        Console.WriteLine();
                        Console.WriteLine("[COLONY INFRASTRUCTURE]");
                        Console.WriteLine($"- Harp Machine          : {(harpMachineBuilt ? "BUILT" : "NOT BUILT")}");
                        Console.WriteLine($"- Spaceport             : {(spaceportBuilt ? "BUILT" : "NOT BUILT")}");

                        if (oxygenFactory != null)
                        {
                            Console.WriteLine($"- Oxygen Factory        : {oxygenFactory.OxygenCubicMeters:F0} / {oxygenFactory.MaximumOxygen:F0} m³");
                        }
                        else
                        {
                            Console.WriteLine("- Oxygen Factory        : NOT BUILT");
                        }

                        Console.WriteLine($"- Fusion Reactors       : {fusionReactors.Count}");
                    }

                    Console.WriteLine();
                    Console.WriteLine("[BASE DEVELOPMENT]");

                    if (!laboratoryExpanded)
                    {
                        Console.WriteLine("- Current Project       : LAB EXTENSION");
                        Console.WriteLine($"- Expansion Turbines    : {windTurbines.Count}/{requiredTurbinesForExpansion}");
                        Console.WriteLine($"- Expansion Basalt      : {basaltMined}/{requiredBasaltForExpansion} Tons");
                        Console.WriteLine($"- Expansion Status      : {(laboratoryExpansionUnlocked ? "UNLOCKED" : "LOCKED")}");
                    }
                    else if (!baseCreated)
                    {
                        Console.WriteLine("- Lab Extension         : COMPLETED");
                        Console.WriteLine();
                        Console.WriteLine("NEXT MISSION OBJECTIVE");
                        Console.WriteLine("PROJECT: BASE CREATION");
                        Console.WriteLine();
                        Console.WriteLine($"- Basalt                : {basaltMined}/{requiredBasaltForBaseCreation} Tons");
                        Console.WriteLine($"- Wind Turbines         : {windTurbines.Count}/{requiredTurbinesForBaseCreation}");
                        Console.WriteLine($"- Base Creation Status  : {(baseCreationUnlocked ? "UNLOCKED" : "LOCKED")}");
                    }
                    else if (!baseExpanded)
                    {
                        Console.WriteLine("- Base Creation         : COMPLETED");
                        Console.WriteLine();
                        Console.WriteLine("NEXT MISSION OBJECTIVE");
                        Console.WriteLine("PROJECT: BASE EXTENSION");
                        Console.WriteLine();
                        Console.WriteLine($"- Basalt                : {basaltMined}/{requiredBasaltForBaseExpansion} Tons");
                        Console.WriteLine($"- Wind Turbines         : {windTurbines.Count}/{requiredTurbinesForBaseExpansion}");
                        Console.WriteLine($"- Hydroponized Production : {hydroponizedProductionKg:F0}/{requiredHydroponizedForBaseExpansion} kg");
                        Console.WriteLine($"- Hydroponized Farms    : {hydroponizedFarms.Count}/{requiredFarmsForBaseExpansion}");
                        Console.WriteLine($"- Food Warehouse        : {(foodWarehouse != null ? 1 : 0)}/{requiredWarehouseForBaseExpansion}");
                        Console.WriteLine($"- Base Extension Status : {(baseExpansionUnlocked ? "UNLOCKED" : "LOCKED")}");
                    }
                    else if (!colonyExpanded)
                    {
                        Console.WriteLine("- Base Extension        : COMPLETED");
                        Console.WriteLine();
                        Console.WriteLine("NEXT MISSION OBJECTIVE");
                        Console.WriteLine("PROJECT: COLONY");
                        Console.WriteLine();
                        Console.WriteLine($"- Basalt                : {basaltMined}/{requiredBasaltForColony} Tons");
                        Console.WriteLine($"- Wind Turbines         : {windTurbines.Count}/{requiredTurbinesForColony}");
                        Console.WriteLine($"- Hydroponized Production : {hydroponizedProductionKg:F0}/{requiredHydroForColony} kg");
                        Console.WriteLine($"- Hydroponized Farms    : {hydroponizedFarms.Count}/{requiredFarmsForColony}");
                        Console.WriteLine($"- Food Warehouse        : {(foodWarehouse != null ? 1 : 0)}/{requiredWarehouseForColony}");
                        Console.WriteLine($"- Food Tons             : {(foodWarehouse != null ? foodWarehouse.CurrentFoodTons : 0):F0}/{requiredFoodTonsForColony}");
                        Console.WriteLine($"- Colony Status         : {(colonyUnlocked ? "UNLOCKED" : "LOCKED")}");
                    }
                    else if (!colonyExtended)
                    {
                        Console.WriteLine("- Colony                : COMPLETED");
                        Console.WriteLine();
                        Console.WriteLine("FINAL MISSION OBJECTIVE");
                        Console.WriteLine("PROJECT: COLONY EXTENSION");
                        Console.WriteLine("(Upon completion, you will be promoted to COLONY GOVERNOR)");
                        Console.WriteLine();
                        Console.WriteLine($"- Basalt                : {basaltMined}/{requiredBasaltForColonyExtension} Tons");
                        Console.WriteLine($"- Wind Turbines         : {windTurbines.Count}/{requiredTurbinesForColonyExtension}");
                        Console.WriteLine($"- Hydroponized Production : {hydroponizedProductionKg:F0}/{requiredHydroForColonyExtension} kg");
                        Console.WriteLine($"- Food Tons             : {(foodWarehouse != null ? foodWarehouse.CurrentFoodTons : 0):F0}/{requiredFoodTonsForColonyExtension}");
                        Console.WriteLine($"- Fusion Reactor        : {fusionReactors.Count}/1");
                        Console.WriteLine($"- Harp Machine          : {(harpMachineBuilt ? "BUILT" : "NOT BUILT")}/1");
                        Console.WriteLine($"- Oxygen Factory        : {(oxygenFactoryBuilt ? "BUILT" : "NOT BUILT")}/1");
                        Console.WriteLine($"- Colony Extension Status : {(colonyExtensionUnlocked ? "UNLOCKED" : "LOCKED")}");
                    }
                    else
                    {
                        Console.WriteLine("- Colony Extension      : COMPLETED");
                        Console.WriteLine();
                        Console.WriteLine("COLONY GOVERNOR STATUS: ACHIEVED");
                    }

                    Console.WriteLine();

                    if (resupplyRequested)
                        Console.WriteLine($"- Resupply Pod Status   : IN TRANSIT (ETA: {resupplyEtaDays} Days)");
                    else
                        Console.WriteLine("- Resupply Pod Status   : STANDBY");

                    // =================================================
                    // UNITS STATUS
                    // =================================================

                    Console.WriteLine();
                    Console.WriteLine("[UNITS STATUS]");

                    Console.WriteLine($"- AR-01 Android : Battery {ar1Battery}% | Condition {ar1Condition}% | Status {(ar1Available ? "AVAILABLE" : ar1.Status)}");
                    Console.WriteLine($"- AR-02 Android : Battery {ar2Battery}% | Condition {ar2Condition}% | Status {(ar2Available ? "AVAILABLE" : ar2.Status)}");

                    if (rover.Status == "BASE" || rover.Status == "RETURNED")
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"- ROVER-01      : Battery {roverBattery}% | Condition {roverCondition}% | Status STANDBY (press R after basalt)");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"- ROVER-01      : Battery {roverBattery}% | Condition {roverCondition}% | Status {rover.Status}");
                    }

                    if (baseCreated && ar3 != null && ar4 != null && rover2 != null)
                    {
                        Console.WriteLine($"- AR-03 Android : Battery {ar3.Battery}% | Condition {ar3.Condition}% | Status {(ar3.Available ? "AVAILABLE" : ar3.Status)}");
                        Console.WriteLine($"- AR-04 Android : Battery {ar4.Battery}% | Condition {ar4.Condition}% | Status {(ar4.Available ? "AVAILABLE" : ar4.Status)}");
                        Console.WriteLine($"- ROVER-02      : Battery {rover2.Battery}% | Condition {rover2.Condition}% | Status {(rover2.Available ? "AVAILABLE" : rover2.Status)}");
                    }

                    if (baseExpanded && ar5 != null && ar6 != null && ar7 != null &&
                        ar8 != null && ar9 != null && rover3 != null && rover4 != null)
                    {
                        Console.WriteLine($"- AR-05 Android : Battery {ar5.Battery}% | Condition {ar5.Condition}% | Status {(ar5.Available ? "AVAILABLE" : ar5.Status)}");
                        Console.WriteLine($"- AR-06 Android : Battery {ar6.Battery}% | Condition {ar6.Condition}% | Status {(ar6.Available ? "AVAILABLE" : ar6.Status)}");
                        Console.WriteLine($"- AR-07 Android : Battery {ar7.Battery}% | Condition {ar7.Condition}% | Status {(ar7.Available ? "AVAILABLE" : ar7.Status)}");
                        Console.WriteLine($"- AR-08 Android : Battery {ar8.Battery}% | Condition {ar8.Condition}% | Status {(ar8.Available ? "AVAILABLE" : ar8.Status)}");
                        Console.WriteLine($"- AR-09 Android : Battery {ar9.Battery}% | Condition {ar9.Condition}% | Status {(ar9.Available ? "AVAILABLE" : ar9.Status)}");
                        Console.WriteLine($"- ROVER-03      : Battery {rover3.Battery}% | Condition {rover3.Condition}% | Status {(rover3.Available ? "AVAILABLE" : rover3.Status)}");
                        Console.WriteLine($"- ROVER-04      : Battery {rover4.Battery}% | Condition {rover4.Condition}% | Status {(rover4.Available ? "AVAILABLE" : rover4.Status)}");
                    }

                    if (colonyExpanded && colonyAndroids.Count > 0)
                    {
                        foreach (FieldUnit android in colonyAndroids)
                        {
                            if (android != null)
                                Console.WriteLine($"- {android.Name,-12}: Battery {android.Battery}% | Condition {android.Condition}% | Status {(android.Available ? "AVAILABLE" : android.Status)}");
                        }

                        if (rover5 != null)
                            Console.WriteLine($"- ROVER-05      : Battery {rover5.Battery}% | Condition {rover5.Condition}% | Status {(rover5.Available ? "AVAILABLE" : rover5.Status)}");

                        if (builder3D != null)
                            Console.WriteLine($"- 3D BUILDER    : Battery {builder3D.Battery}% | Condition {builder3D.Condition}% | Status {(builder3D.Available ? "AVAILABLE" : builder3D.Status)}");
                    }

                    if (colonyExtended && rover6 != null)
                    {
                        Console.WriteLine($"- ROVER-06      : Battery {rover6.Battery}% | Condition {rover6.Condition}% | Status {(rover6.Available ? "AVAILABLE" : rover6.Status)}");
                    }

                    // =================================================
                    // COMMAND OPTIONS
                    // =================================================

                    Console.WriteLine();
                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine("COMMAND OPTIONS:");

                    Console.WriteLine("1. Deploy Field Units                    (Independent Android Basalt Search)");
                    Console.WriteLine("2. Construct Wind Turbine (W)            (Assembly, +Variable Power)");

                    if (!laboratoryExpanded)
                        Console.WriteLine("3. Expand Lab                            (3 Turbines + 120 Tons Basalt)");
                    else if (!baseCreated)
                        Console.WriteLine("3. Create Base                           (3 Turbines + 120 Tons Basalt)");
                    else if (!baseExpanded)
                        Console.WriteLine("3. Expand Base                           (6 Turbines + 300 Tons + 200 kg + 2 Farms + 1 Warehouse)");
                    else if (!colonyExpanded)
                        Console.WriteLine("3. Create Colony                         (12 Turbines + 1200 Tons + 800 kg + 10 Food)");
                    else if (!colonyExtended)
                        Console.WriteLine("3. Extend Colony                         (17 Turbines + 2000 Tons + 1500 kg + 25 Food + Fusion + Harp + Oxygen)");
                    else
                        Console.WriteLine("3. Colony at MAX LEVEL                   (You are Colony Governor)");

                    Console.WriteLine("4. Call Emergency Resupply Pod           (Delivers Food/Oxygen in 3 Days)");
                    Console.WriteLine("5. Recharge Units & End Day              (Consumes 1 Day)");
                    Console.WriteLine("6. Abort / Save & Exit Mission");

                    if (baseCreated)
                    {
                        Console.WriteLine("7. Build Hydroponized Farm               (300 Tons Basalt)");
                        Console.WriteLine("8. Build Food Warehouse                  (Capacity 30 Tons)");
                    }

                    if (colonyExpanded)
                    {
                        Console.WriteLine("9.  Build 3D Builder (D)                 (Free - Builder Unit)");
                        Console.WriteLine("10. Build Harp Machine (H)               (2 Android + 1 Rover + 20 Personnel + 1 D)");
                        Console.WriteLine("11. Build Spaceport (P)                  (2 Android + 1 Rover + 20 Personnel + 1 D)");
                        Console.WriteLine("12. Build Oxygen Factory (O)             (2 Android + 1 Rover + 20 Personnel + 1 D)");
                        Console.WriteLine("13. Build Fusion Reactor (R)             (2 Android + 1 Rover + 20 Personnel + 1 D)");
                    }

                    Console.WriteLine("---------------------------------------------------------------------------------");

                    Console.Write("Enter Command: ");

                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            ExecuteFieldOperation(ref grid, drillingX, drillingY,
                                ref basaltX, ref basaltY, ref basaltDiscovered, ref basaltReserveTons, ref basaltMined,
                                ref ar1Available, ref ar2Available, ref ar1Battery, ref ar2Battery, ref roverBattery,
                                ref ar1Condition, ref ar2Condition, ref roverCondition,
                                ar1, ar2, rover, rand, maxBasaltStorage, fieldTickMilliseconds);
                            break;

                        case "2":
                            ExecuteWindTurbineConstruction(ref grid, drillingX, drillingY,
                                windTurbines, rand, ref currentDay, ref basePower, ref foodRationsDays,
                                ref foodConsumptionAccumulator, ref lifeSupportDays, ref availableCrew, ref assignedCrew,
                                ref ar1Available, ref ar2Available, ref ar1Battery, ref ar2Battery,
                                ref ar1Condition, ref ar2Condition);
                            break;

                        case "3":
                            ExecuteExpansion(ref grid, windTurbines, fusionReactors, rand,
                                ref basaltMined, ref totalCrew, ref availableCrew, ref assignedCrew,
                                ref currentDay, ref foodRationsDays, ref foodConsumptionAccumulator,
                                ref lifeSupportDays, ref basePower,
                                ref laboratoryExpanded, ref laboratoryExpansionUnlocked,
                                ref baseCreated, ref baseCreationUnlocked,
                                ref baseExpanded, ref baseExpansionUnlocked,
                                ref colonyExpanded, ref colonyUnlocked,
                                ref colonyExtended, ref colonyExtensionUnlocked,
                                ref hydroponizedProductionKg, ref hydroponizedProductionStarted,
                                ref hydroponizedProductionStartTime,
                                ref ar3, ref ar4, ref rover2,
                                ref ar5, ref ar6, ref ar7, ref ar8, ref ar9,
                                ref rover3, ref rover4,
                                colonyAndroids, ref rover5, ref rover6, ref builder3D,
                                ref maxBasaltStorage,
                                requiredBasaltForExpansion, requiredTurbinesForExpansion,
                                requiredBasaltForBaseCreation, requiredTurbinesForBaseCreation,
                                requiredBasaltForBaseExpansion, requiredTurbinesForBaseExpansion,
                                requiredHydroponizedForBaseExpansion,
                                requiredFarmsForBaseExpansion, requiredWarehouseForBaseExpansion,
                                requiredBasaltForColony, requiredTurbinesForColony,
                                requiredHydroForColony, requiredFarmsForColony,
                                requiredWarehouseForColony, requiredFoodTonsForColony,
                                requiredBasaltForColonyExtension,
                                requiredTurbinesForColonyExtension,
                                requiredHydroForColonyExtension,
                                requiredFoodTonsForColonyExtension,
                                hydroponizedFarms, foodWarehouse,
                                harpMachineBuilt, oxygenFactoryBuilt,
                                fusionReactors,
                                drillingX, drillingY);
                            break;

                        case "4":
                            ExecuteResupply(ref resupplyRequested, ref resupplyEtaDays);
                            break;

                        case "5":
                            ExecuteEndDay(ref currentDay, ref basePower, ref foodRationsDays,
                                ref lifeSupportDays, ref foodConsumptionAccumulator,
                                ref ar1Battery, ref ar2Battery, ref roverBattery,
                                ar3, ar4, rover2, ar5, ar6, ar7, ar8, ar9, rover3, rover4,
                                colonyAndroids, rover5, rover6, builder3D,
                                windTurbines, fusionReactors, rand, hydroponizedProductionKg,
                                ref resupplyRequested, ref resupplyEtaDays,
                                ref missionActive, laboratoryExpanded, baseCreated, baseExpanded, colonyExpanded, colonyExtended);
                            break;

                        case "6":
                            missionActive = false;
                            Console.WriteLine();
                            Console.WriteLine("Exiting Mission 02 Tactical Mode.");
                            Console.WriteLine("Goodbye Commander!");
                            break;

                        case "7":
                            if (!baseCreated)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Hydroponized Farms require Base Creation first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            ExecuteFarmConstruction(ref grid, hydroponizedFarms,
                                ref basaltMined, ref availableCrew, ref assignedCrew,
                                ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
                                ref currentDay, ref foodRationsDays, ref foodConsumptionAccumulator,
                                ref lifeSupportDays, ref basePower, maxBasaltStorage);
                            break;

                        case "8":
                            if (!baseCreated)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Food Warehouse requires Base Creation first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            ExecuteWarehouseConstruction(ref grid, ref foodWarehouse,
                                ref availableCrew, ref assignedCrew,
                                ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
                                ref currentDay, ref foodRationsDays, ref foodConsumptionAccumulator,
                                ref lifeSupportDays, ref basePower);
                            break;

                        case "9":
                            if (!colonyExpanded)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: 3D Builder requires Colony first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            Execute3DBuilderConstruction(ref grid, ref builder3D,
                                ref availableCrew, ref assignedCrew,
                                colonyAndroids, rover5,
                                ref currentDay, ref foodRationsDays,
                                ref foodConsumptionAccumulator, ref lifeSupportDays, ref basePower);
                            break;

                        case "10":
                            if (!colonyExpanded)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Harp Machine requires Colony first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            ExecuteHarpMachineConstruction(ref grid, ref harpMachine, ref harpMachineBuilt,
                                ref availableCrew, ref assignedCrew,
                                colonyAndroids, rover5, builder3D,
                                ref currentDay, ref foodRationsDays,
                                ref foodConsumptionAccumulator, ref lifeSupportDays, ref basePower);
                            break;

                        case "11":
                            if (!colonyExpanded)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Spaceport requires Colony first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            ExecuteSpaceportConstruction(ref grid, ref spaceport, ref spaceportBuilt,
                                ref availableCrew, ref assignedCrew,
                                colonyAndroids, rover5, builder3D,
                                ref currentDay, ref foodRationsDays,
                                ref foodConsumptionAccumulator, ref lifeSupportDays, ref basePower);
                            break;

                        case "12":
                            if (!colonyExpanded)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Oxygen Factory requires Colony first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            ExecuteOxygenFactoryConstruction(ref grid, ref oxygenFactory, ref oxygenFactoryBuilt,
                                ref availableCrew, ref assignedCrew,
                                colonyAndroids, rover5, builder3D,
                                ref currentDay, ref foodRationsDays,
                                ref foodConsumptionAccumulator, ref lifeSupportDays, ref basePower);
                            break;

                        case "13":
                            if (!colonyExpanded)
                            {
                                Console.WriteLine();
                                Console.WriteLine("ERROR: Fusion Reactor requires Colony first.");
                                Thread.Sleep(1500);
                                break;
                            }

                            ExecuteFusionReactorConstruction(ref grid, fusionReactors,
                                windTurbines,
                                ref availableCrew, ref assignedCrew,
                                colonyAndroids, rover5, builder3D,
                                ref currentDay, ref foodRationsDays,
                                ref foodConsumptionAccumulator, ref lifeSupportDays, ref basePower);
                            break;

                        default:
                            Console.WriteLine();
                            Console.WriteLine("Invalid input! Select 1-13.");
                            Thread.Sleep(1000);
                            break;
                    }

                    if (baseExpanded &&
                        !finalBaseObjectiveCompleted &&
                        hydroponizedFarms.Count >= requiredFarmsForFinalBase &&
                        foodWarehouse != null &&
                        windTurbines.Count >= requiredTurbinesForBaseExpansion + requiredAdditionalTurbinesForFinalBase &&
                        basaltMined >= requiredFinalBasalt)
                    {
                        finalBaseObjectiveCompleted = true;
                        maxBasaltStorage += 500;

                        Console.Clear();
                        DrawGrid(grid, new List<FieldUnit>());
                        Console.WriteLine();
                        Console.WriteLine("==================================================");
                        Console.WriteLine("          APHRODITE BASE - FINAL OBJECTIVE");
                        Console.WriteLine("==================================================");
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("CONGRATULATIONS COMMANDER!");
                        Console.WriteLine();
                        Console.WriteLine("THE APHRODITE BASE HAS REACHED FULL");
                        Console.WriteLine("COLONIZATION INFRASTRUCTURE STATUS.");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("- Hydroponized Farms : 2/2");
                        Console.WriteLine("- Food Warehouse     : 1/1");
                        Console.WriteLine("- Wind Turbines      : 9");
                        Console.WriteLine("- Basalt Storage     : 1000+ Tons");
                        Console.WriteLine("- Personnel          : 115");
                        Console.WriteLine();
                        Console.WriteLine("FINAL BASE DEVELOPMENT OBJECTIVE COMPLETE.");
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("==================================================");
                    Console.WriteLine("                    RUNTIME ERROR!");
                    Console.WriteLine("==================================================");
                    Console.WriteLine();
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.WriteLine();
                    Console.WriteLine($"Stack Trace:");
                    Console.WriteLine(ex.StackTrace);
                    Console.ResetColor();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                    return;
                }
            }

            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("          MISSION COMPLETE");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("==================================================");
            Console.WriteLine("                    FATAL ERROR!");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine($"Error: {ex.Message}");
            Console.WriteLine();
            Console.WriteLine($"Stack Trace:");
            Console.WriteLine(ex.StackTrace);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
    
    // =========================================================
    // EXECUTE EXPANSION - UPDATED (Lab Extension / Base Creation / Base Extension / Colony / Colony Extension)
    // =========================================================

    static void ExecuteExpansion(
        ref char[,] grid,
        List<WindTurbine> windTurbines,
        List<FusionReactor> fusionReactors,
        Random rand,
        ref int basaltMined,
        ref int totalCrew,
        ref int availableCrew,
        ref int assignedCrew,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower,
        ref bool laboratoryExpanded,
        ref bool laboratoryExpansionUnlocked,
        ref bool baseCreated,
        ref bool baseCreationUnlocked,
        ref bool baseExpanded,
        ref bool baseExpansionUnlocked,
        ref bool colonyExpanded,
        ref bool colonyUnlocked,
        ref bool colonyExtended,
        ref bool colonyExtensionUnlocked,
        ref double hydroponizedProductionKg,
        ref bool hydroponizedProductionStarted,
        ref DateTime hydroponizedProductionStartTime,
        ref FieldUnit ar3,
        ref FieldUnit ar4,
        ref FieldUnit rover2,
        ref FieldUnit ar5,
        ref FieldUnit ar6,
        ref FieldUnit ar7,
        ref FieldUnit ar8,
        ref FieldUnit ar9,
        ref FieldUnit rover3,
        ref FieldUnit rover4,
        List<FieldUnit> colonyAndroids,
        ref FieldUnit rover5,
        ref FieldUnit rover6,
        ref FieldUnit builder3D,
        ref int maxBasaltStorage,
        int requiredBasaltForExpansion,
        int requiredTurbinesForExpansion,
        int requiredBasaltForBaseCreation,
        int requiredTurbinesForBaseCreation,
        int requiredBasaltForBaseExpansion,
        int requiredTurbinesForBaseExpansion,
        int requiredHydroponizedForBaseExpansion,
        int requiredFarmsForBaseExpansion,
        int requiredWarehouseForBaseExpansion,
        int requiredBasaltForColony,
        int requiredTurbinesForColony,
        int requiredHydroForColony,
        int requiredFarmsForColony,
        int requiredWarehouseForColony,
        int requiredFoodTonsForColony,
        int requiredBasaltForColonyExtension,
        int requiredTurbinesForColonyExtension,
        int requiredHydroForColonyExtension,
        int requiredFoodTonsForColonyExtension,
        List<HydroponizedFarm> hydroponizedFarms,
        FoodWarehouse foodWarehouse,
        bool harpMachineBuilt,
        bool oxygenFactoryBuilt,
        List<FusionReactor> fusionReactorsList,
        int drillingX,
        int drillingY)
    {
        // =================================================
        // LABORATORY (LAB) EXPANSION
        // =================================================

        if (!laboratoryExpanded)
        {
            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("              LAB EXTENSION");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("LAB EXTENSION REQUIREMENTS");
            Console.WriteLine();
            Console.WriteLine($"Wind Turbines : {windTurbines.Count} / {requiredTurbinesForExpansion}");
            Console.WriteLine($"Basalt        : {basaltMined} / {requiredBasaltForExpansion} Tons");
            Console.WriteLine();

            if (windTurbines.Count < requiredTurbinesForExpansion ||
                basaltMined < requiredBasaltForExpansion)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Requirements not met.");
                Console.ResetColor();
                Console.WriteLine();

                if (windTurbines.Count < requiredTurbinesForExpansion)
                    Console.WriteLine($"Need {requiredTurbinesForExpansion - windTurbines.Count} more Wind Turbine(s).");

                if (basaltMined < requiredBasaltForExpansion)
                    Console.WriteLine($"Need {requiredBasaltForExpansion - basaltMined} more Tons of Basalt.");

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            laboratoryExpansionUnlocked = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Requirements met.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Lab Extension will consume:");
            Console.WriteLine($"Basalt            : {requiredBasaltForExpansion} Tons");
            Console.WriteLine("Construction Time : 1 Day");
            Console.WriteLine("Crew Expansion    : +8 Personnel");
            Console.WriteLine();

            Console.Write("Proceed with Lab Extension? Y/N: ");
            string expansionInput = Console.ReadLine();

            if (expansionInput == null || expansionInput.ToUpper() != "Y")
            {
                Console.WriteLine();
                Console.WriteLine("Lab Extension cancelled.");
                Thread.Sleep(1200);
                return;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("       LAB EXTENSION IN PROGRESS");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("[20%] Structural excavation...");
            Thread.Sleep(2000);
            Console.WriteLine("[40%] Underground chamber construction...");
            Thread.Sleep(2000);
            Console.WriteLine("[60%] Life support expansion...");
            Thread.Sleep(2000);
            Console.WriteLine("[80%] Power infrastructure...");
            Thread.Sleep(2000);
            Console.WriteLine("[100%] Systems integration...");
            Thread.Sleep(2000);

            basaltMined -= requiredBasaltForExpansion;
            maxBasaltStorage += 150;

            totalCrew += 8;
            availableCrew += 8;

            currentDay++;
            ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, hydroponizedProductionKg);
            lifeSupportDays--;
            basePower -= 10;

            UpdateWindTurbines(windTurbines, rand);

            double expansionWindPower = 0;

            foreach (WindTurbine turbine in windTurbines)
                expansionWindPower += turbine.CurrentPower;

            basePower += (int)Math.Round(expansionWindPower);
            basePower = Math.Min(100, Math.Max(0, basePower));

            laboratoryExpanded = true;
            laboratoryExpansionUnlocked = false;

            hydroponizedProductionKg = 0;
            hydroponizedProductionStarted = true;
            hydroponizedProductionStartTime = DateTime.Now;

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("       LAB EXTENSION COMPLETE");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CONGRATULATIONS COMMANDER! LAB EXTENDED.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"- Basalt consumed: {requiredBasaltForExpansion} Tons");
            Console.WriteLine($"- Personnel: +8 (Total: {totalCrew})");
            Console.WriteLine();
            Console.WriteLine("NEXT: BASE CREATION (3 Turbines + 120 Tons Basalt)");
            Console.WriteLine();
            Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // =================================================
        // BASE CREATION
        // =================================================

        if (!baseCreated)
        {
            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("                BASE CREATION");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("BASE CREATION REQUIREMENTS");
            Console.WriteLine();
            Console.WriteLine($"Wind Turbines : {windTurbines.Count} / {requiredTurbinesForBaseCreation}");
            Console.WriteLine($"Basalt        : {basaltMined} / {requiredBasaltForBaseCreation} Tons");
            Console.WriteLine();

            if (windTurbines.Count < requiredTurbinesForBaseCreation ||
                basaltMined < requiredBasaltForBaseCreation)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Requirements not met.");
                Console.ResetColor();
                Console.WriteLine();

                if (windTurbines.Count < requiredTurbinesForBaseCreation)
                    Console.WriteLine($"Need {requiredTurbinesForBaseCreation - windTurbines.Count} more Wind Turbine(s).");

                if (basaltMined < requiredBasaltForBaseCreation)
                    Console.WriteLine($"Need {requiredBasaltForBaseCreation - basaltMined} more Tons of Basalt.");

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            baseCreationUnlocked = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Requirements met.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Base Creation will consume:");
            Console.WriteLine($"Basalt            : {requiredBasaltForBaseCreation} Tons");
            Console.WriteLine("Construction Time : 1 Day");
            Console.WriteLine("Crew Expansion    : +200 Personnel");
            Console.WriteLine();
            Console.WriteLine("New Units: AR-03, AR-04, ROVER-02");
            Console.WriteLine("Unlocks: Hydroponized Farm (7), Food Warehouse (8)");
            Console.WriteLine();

            Console.Write("Proceed with Base Creation? Y/N: ");
            string creationInput = Console.ReadLine();

            if (creationInput == null || creationInput.ToUpper() != "Y")
            {
                Console.WriteLine();
                Console.WriteLine("Base Creation cancelled.");
                Thread.Sleep(1200);
                return;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("           BASE CREATION IN PROGRESS");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("[20%] Foundation preparation...");
            Thread.Sleep(2000);
            Console.WriteLine("[40%] Base structure assembly...");
            Thread.Sleep(2000);
            Console.WriteLine("[60%] Personnel habitation setup...");
            Thread.Sleep(2000);
            Console.WriteLine("[80%] Industrial systems installation...");
            Thread.Sleep(2000);
            Console.WriteLine("[100%] Base command systems online...");
            Thread.Sleep(2000);

            basaltMined -= requiredBasaltForBaseCreation;
            maxBasaltStorage += 150;

            grid[drillingY, drillingX] = 'B';

            totalCrew += 200;
            availableCrew += 200;

            ar3 = new FieldUnit("AR-03", 'A');
            ar4 = new FieldUnit("AR-04", 'A');
            rover2 = new FieldUnit("ROVER-02", 'r');

            ar3.Status = "BASE";
            ar4.Status = "BASE";
            rover2.Status = "BASE";
            ar3.Available = true;
            ar4.Available = true;
            rover2.Available = true;

            currentDay++;
            ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, hydroponizedProductionKg);
            lifeSupportDays--;
            basePower -= 10;

            UpdateWindTurbines(windTurbines, rand);

            double baseCreationWindPower = 0;

            foreach (WindTurbine turbine in windTurbines)
                baseCreationWindPower += turbine.CurrentPower;

            basePower += (int)Math.Round(baseCreationWindPower);
            basePower = Math.Min(100, Math.Max(0, basePower));

            baseCreated = true;
            baseCreationUnlocked = false;

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("            BASE CREATION COMPLETE");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CONGRATULATIONS COMMANDER!");
            Console.WriteLine("APHRODITE BASE HAS BEEN CREATED.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"- Laboratory upgraded to Main Base (B)");
            Console.WriteLine($"- Personnel: +200 (Total: {totalCrew})");
            Console.WriteLine($"- New Androids: AR-03, AR-04");
            Console.WriteLine($"- New Rover: ROVER-02");
            Console.WriteLine();
            Console.WriteLine("NEW OPTIONS UNLOCKED:");
            Console.WriteLine("7. Build Hydroponized Farm");
            Console.WriteLine("8. Build Food Warehouse");
            Console.WriteLine();
            Console.WriteLine("NEXT: BASE EXTENSION (6 Turbines + 300 Tons + 200 kg + 2 Farms + 1 Warehouse)");
            Console.WriteLine();
            Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // =================================================
        // BASE EXTENSION
        // =================================================

        if (!baseExpanded)
        {
            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("                BASE EXTENSION");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("BASE EXTENSION REQUIREMENTS");
            Console.WriteLine();
            Console.WriteLine($"Wind Turbines           : {windTurbines.Count}/{requiredTurbinesForBaseExpansion}");
            Console.WriteLine($"Basalt                  : {basaltMined}/{requiredBasaltForBaseExpansion} Tons");
            Console.WriteLine($"Hydroponized Production : {hydroponizedProductionKg:F0}/{requiredHydroponizedForBaseExpansion} kg");
            Console.WriteLine($"Hydroponized Farms      : {hydroponizedFarms.Count}/{requiredFarmsForBaseExpansion}");
            Console.WriteLine($"Food Warehouse          : {(foodWarehouse != null ? 1 : 0)}/{requiredWarehouseForBaseExpansion}");
            Console.WriteLine();

            bool reqMet = true;

            if (windTurbines.Count < requiredTurbinesForBaseExpansion)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredTurbinesForBaseExpansion - windTurbines.Count} more Wind Turbine(s).");
                Console.ResetColor();
                reqMet = false;
            }

            if (basaltMined < requiredBasaltForBaseExpansion)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredBasaltForBaseExpansion - basaltMined} more Tons of Basalt.");
                Console.ResetColor();
                reqMet = false;
            }

            if (hydroponizedProductionKg < requiredHydroponizedForBaseExpansion)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredHydroponizedForBaseExpansion - hydroponizedProductionKg:F0} kg more Hydroponized Production.");
                Console.ResetColor();
                reqMet = false;
            }

            if (hydroponizedFarms.Count < requiredFarmsForBaseExpansion)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredFarmsForBaseExpansion - hydroponizedFarms.Count} more Hydroponized Farm(s).");
                Console.ResetColor();
                reqMet = false;
            }

            if (foodWarehouse == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Need 1 Food Warehouse.");
                Console.ResetColor();
                reqMet = false;
            }

            Console.WriteLine();

            if (!reqMet)
            {
                Console.WriteLine("Base Extension is currently LOCKED.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            baseExpansionUnlocked = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("BASE EXTENSION REQUIREMENTS MET.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("BASE EXTENSION WILL CONSUME:");
            Console.WriteLine("- 300 Tons Basalt");
            Console.WriteLine("- 1 Day Construction");
            Console.WriteLine("- +100 Personnel");
            Console.WriteLine("- 5 Androids (AR-05 to AR-09)");
            Console.WriteLine("- 2 Rovers (ROVER-03, ROVER-04)");
            Console.WriteLine();

            Console.Write("Proceed with Base Extension? Y/N: ");
            string baseInput = Console.ReadLine();

            if (baseInput == null || baseInput.ToUpper() != "Y")
            {
                Console.WriteLine();
                Console.WriteLine("Base Extension cancelled.");
                Thread.Sleep(1200);
                return;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("          BASE EXTENSION IN PROGRESS");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("[20%] Surface reinforcement...");
            Thread.Sleep(3000);
            Console.WriteLine("[40%] Main Base structural construction...");
            Thread.Sleep(3000);
            Console.WriteLine("[60%] Personnel habitation modules...");
            Thread.Sleep(3000);
            Console.WriteLine("[80%] Industrial systems installation...");
            Thread.Sleep(3000);
            Console.WriteLine("[100%] Base command systems online...");
            Thread.Sleep(3000);

            basaltMined -= 300;
            maxBasaltStorage += 200;

            totalCrew += 100;
            availableCrew += 100;

            ar5 = new FieldUnit("AR-05", 'A');
            ar6 = new FieldUnit("AR-06", 'A');
            ar7 = new FieldUnit("AR-07", 'A');
            ar8 = new FieldUnit("AR-08", 'A');
            ar9 = new FieldUnit("AR-09", 'A');

            ar5.Status = "BASE";
            ar6.Status = "BASE";
            ar7.Status = "BASE";
            ar8.Status = "BASE";
            ar9.Status = "BASE";

            ar5.Available = true;
            ar6.Available = true;
            ar7.Available = true;
            ar8.Available = true;
            ar9.Available = true;

            rover3 = new FieldUnit("ROVER-03", 'r');
            rover4 = new FieldUnit("ROVER-04", 'r');
            rover3.Status = "BASE";
            rover4.Status = "BASE";
            rover3.Available = true;
            rover4.Available = true;

            currentDay++;
            ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
            lifeSupportDays--;
            basePower -= 10;

            UpdateWindTurbines(windTurbines, rand);

            double baseExpansionWindPower = 0;

            foreach (WindTurbine turbine in windTurbines)
                baseExpansionWindPower += turbine.CurrentPower;

            basePower += (int)Math.Round(baseExpansionWindPower);
            basePower = Math.Min(100, Math.Max(0, basePower));

            baseExpanded = true;
            baseExpansionUnlocked = false;

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("            BASE EXTENSION COMPLETE");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CONGRATULATIONS COMMANDER!");
            Console.WriteLine("APHRODITE BASE HAS BEEN EXTENDED.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"- Personnel: +100 (Total: {totalCrew})");
            Console.WriteLine($"- New Androids: AR-05, AR-06, AR-07, AR-08, AR-09");
            Console.WriteLine($"- New Rovers: ROVER-03, ROVER-04");
            Console.WriteLine();
            Console.WriteLine("NEXT: COLONY (12 Turbines + 1200 Tons + 800 kg + 10 Food)");
            Console.WriteLine();
            Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // =================================================
        // COLONY
        // =================================================

        if (!colonyExpanded)
        {
            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("                COLONY");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("COLONY REQUIREMENTS");
            Console.WriteLine();
            Console.WriteLine($"Wind Turbines           : {windTurbines.Count}/{requiredTurbinesForColony}");
            Console.WriteLine($"Basalt                  : {basaltMined}/{requiredBasaltForColony} Tons");
            Console.WriteLine($"Hydroponized Production : {hydroponizedProductionKg:F0}/{requiredHydroForColony} kg");
            Console.WriteLine($"Hydroponized Farms      : {hydroponizedFarms.Count}/{requiredFarmsForColony}");
            Console.WriteLine($"Food Warehouse          : {(foodWarehouse != null ? 1 : 0)}/{requiredWarehouseForColony}");
            Console.WriteLine($"Food Tons               : {(foodWarehouse != null ? foodWarehouse.CurrentFoodTons : 0):F0}/{requiredFoodTonsForColony}");
            Console.WriteLine();

            bool reqMet = true;

            if (windTurbines.Count < requiredTurbinesForColony)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredTurbinesForColony - windTurbines.Count} more Wind Turbine(s).");
                Console.ResetColor();
                reqMet = false;
            }

            if (basaltMined < requiredBasaltForColony)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredBasaltForColony - basaltMined} more Tons of Basalt.");
                Console.ResetColor();
                reqMet = false;
            }

            if (hydroponizedProductionKg < requiredHydroForColony)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredHydroForColony - hydroponizedProductionKg:F0} kg more Hydroponized Production.");
                Console.ResetColor();
                reqMet = false;
            }

            if (hydroponizedFarms.Count < requiredFarmsForColony)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredFarmsForColony - hydroponizedFarms.Count} more Hydroponized Farm(s).");
                Console.ResetColor();
                reqMet = false;
            }

            if (foodWarehouse == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Need 1 Food Warehouse.");
                Console.ResetColor();
                reqMet = false;
            }

            if (foodWarehouse != null && foodWarehouse.CurrentFoodTons < requiredFoodTonsForColony)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredFoodTonsForColony - foodWarehouse.CurrentFoodTons:F0} more Tons of Food in Warehouse.");
                Console.ResetColor();
                reqMet = false;
            }

            Console.WriteLine();

            if (!reqMet)
            {
                Console.WriteLine("Colony is currently LOCKED.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            colonyUnlocked = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("COLONY REQUIREMENTS MET.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("COLONY WILL CONSUME:");
            Console.WriteLine($"- {requiredBasaltForColony} Tons Basalt");
            Console.WriteLine("- 1 Day Construction");
            Console.WriteLine("- +1000 Colonists");
            Console.WriteLine("- 10 Androids (AR-10 to AR-19)");
            Console.WriteLine("- 1 Rover (ROVER-05)");
            Console.WriteLine();

            Console.Write("Proceed with Colony? Y/N: ");
            string colonyInput = Console.ReadLine();

            if (colonyInput == null || colonyInput.ToUpper() != "Y")
            {
                Console.WriteLine();
                Console.WriteLine("Colony cancelled.");
                Thread.Sleep(1200);
                return;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("          COLONY IN PROGRESS");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("[20%] Colony foundation...");
            Thread.Sleep(3000);
            Console.WriteLine("[40%] Habitat modules construction...");
            Thread.Sleep(3000);
            Console.WriteLine("[60%] Water and oxygen infrastructure...");
            Thread.Sleep(3000);
            Console.WriteLine("[80%] Colony command systems...");
            Thread.Sleep(3000);
            Console.WriteLine("[100%] Colony systems online...");
            Thread.Sleep(3000);

            basaltMined -= requiredBasaltForColony;
            maxBasaltStorage += 800;

            grid[drillingY, drillingX] = 'C';

            totalCrew += 1000;
            availableCrew += 1000;

            for (int i = 10; i <= 19; i++)
            {
                FieldUnit android = new FieldUnit($"AR-{i:00}", 'A');
                android.Status = "BASE";
                android.Available = true;
                colonyAndroids.Add(android);
            }

            rover5 = new FieldUnit("ROVER-05", 'r');
            rover5.Status = "BASE";
            rover5.Available = true;

            currentDay++;
            ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
            lifeSupportDays--;
            basePower -= 10;

            UpdateWindTurbines(windTurbines, rand);

            double colonyWindPower = 0;

            foreach (WindTurbine turbine in windTurbines)
                colonyWindPower += turbine.CurrentPower;

            basePower += (int)Math.Round(colonyWindPower);
            basePower = Math.Min(100, Math.Max(0, basePower));

            colonyExpanded = true;
            colonyUnlocked = false;

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("            COLONY COMPLETE");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("CONGRATULATIONS COMMANDER!");
            Console.WriteLine("APHRODITE COLONY HAS BEEN ESTABLISHED.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine($"- Base upgraded to Colony (C)");
            Console.WriteLine($"- Personnel: +1000 (Total: {totalCrew})");
            Console.WriteLine($"- New Androids: AR-10 to AR-19");
            Console.WriteLine($"- New Rover: ROVER-05");
            Console.WriteLine($"- Basalt Storage: 2000 Tons");
            Console.WriteLine();
            Console.WriteLine("NEW CONSTRUCTION OPTIONS UNLOCKED:");
            Console.WriteLine("9.  Build 3D Builder (D)");
            Console.WriteLine("10. Build Harp Machine (H)");
            Console.WriteLine("11. Build Spaceport (P)");
            Console.WriteLine("12. Build Oxygen Factory (O)");
            Console.WriteLine("13. Build Fusion Reactor (R)");
            Console.WriteLine();
            Console.WriteLine("FINAL MISSION: COLONY EXTENSION");
            Console.WriteLine("(17 Turbines + 2000 Tons + 1500 kg + 25 Food + Fusion + Harp + Oxygen)");
            Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }

        // =================================================
        // COLONY EXTENSION (FINAL) - NEW
        // =================================================

        if (!colonyExtended)
        {
            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("            COLONY EXTENSION");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("FINAL MISSION OBJECTIVE");
            Console.WriteLine("(Upon completion, you will be promoted to COLONY GOVERNOR)");
            Console.WriteLine();
            Console.WriteLine("COLONY EXTENSION REQUIREMENTS");
            Console.WriteLine();
            Console.WriteLine($"Wind Turbines           : {windTurbines.Count}/{requiredTurbinesForColonyExtension}");
            Console.WriteLine($"Basalt                  : {basaltMined}/{requiredBasaltForColonyExtension} Tons");
            Console.WriteLine($"Hydroponized Production : {hydroponizedProductionKg:F0}/{requiredHydroForColonyExtension} kg");
            Console.WriteLine($"Food Tons               : {(foodWarehouse != null ? foodWarehouse.CurrentFoodTons : 0):F0}/{requiredFoodTonsForColonyExtension}");
            Console.WriteLine($"Fusion Reactor          : {fusionReactorsList.Count}/1");
            Console.WriteLine($"Harp Machine            : {(harpMachineBuilt ? "BUILT" : "NOT BUILT")}/1");
            Console.WriteLine($"Oxygen Factory          : {(oxygenFactoryBuilt ? "BUILT" : "NOT BUILT")}/1");
            Console.WriteLine();

            bool reqMet = true;

            if (windTurbines.Count < requiredTurbinesForColonyExtension)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredTurbinesForColonyExtension - windTurbines.Count} more Wind Turbine(s).");
                Console.ResetColor();
                reqMet = false;
            }

            if (basaltMined < requiredBasaltForColonyExtension)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredBasaltForColonyExtension - basaltMined} more Tons of Basalt.");
                Console.ResetColor();
                reqMet = false;
            }

            if (hydroponizedProductionKg < requiredHydroForColonyExtension)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Need {requiredHydroForColonyExtension - hydroponizedProductionKg:F0} kg more Hydroponized Production.");
                Console.ResetColor();
                reqMet = false;
            }

            if (foodWarehouse == null || foodWarehouse.CurrentFoodTons < requiredFoodTonsForColonyExtension)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (foodWarehouse == null)
                    Console.WriteLine("Need 1 Food Warehouse with 25 Tons of Food.");
                else
                    Console.WriteLine($"Need {requiredFoodTonsForColonyExtension - foodWarehouse.CurrentFoodTons:F0} more Tons of Food in Warehouse.");
                Console.ResetColor();
                reqMet = false;
            }

            if (fusionReactorsList.Count < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Need 1 Fusion Reactor.");
                Console.ResetColor();
                reqMet = false;
            }

            if (!harpMachineBuilt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Need 1 Harp Machine.");
                Console.ResetColor();
                reqMet = false;
            }

            if (!oxygenFactoryBuilt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Need 1 Oxygen Factory.");
                Console.ResetColor();
                reqMet = false;
            }

            Console.WriteLine();

            if (!reqMet)
            {
                Console.WriteLine("Colony Extension is currently LOCKED.");
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            colonyExtensionUnlocked = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("COLONY EXTENSION REQUIREMENTS MET.");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("COLONY EXTENSION WILL CONSUME:");
            Console.WriteLine($"- {requiredBasaltForColonyExtension} Tons Basalt");
            Console.WriteLine("- 1 Day Construction");
            Console.WriteLine("- +2000 Colonists");
            Console.WriteLine("- 10 Androids (AR-20 to AR-29)");
            Console.WriteLine("- 1 Rover (ROVER-06)");
            Console.WriteLine();

            Console.Write("Proceed with Colony Extension? Y/N: ");
            string extensionInput = Console.ReadLine();

            if (extensionInput == null || extensionInput.ToUpper() != "Y")
            {
                Console.WriteLine();
                Console.WriteLine("Colony Extension cancelled.");
                Thread.Sleep(1200);
                return;
            }

            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("       COLONY EXTENSION IN PROGRESS");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine("[20%] Deep foundation excavation...");
            Thread.Sleep(3000);
            Console.WriteLine("[40%] Extended habitat construction...");
            Thread.Sleep(3000);
            Console.WriteLine("[60%] Advanced infrastructure...");
            Thread.Sleep(3000);
            Console.WriteLine("[80%] Colony capital systems...");
            Thread.Sleep(3000);
            Console.WriteLine("[100%] Colony Extension complete...");
            Thread.Sleep(3000);

            basaltMined -= requiredBasaltForColonyExtension;
            maxBasaltStorage += 1000;

            totalCrew += 2000;
            availableCrew += 2000;

            for (int i = 20; i <= 29; i++)
            {
                FieldUnit android = new FieldUnit($"AR-{i:00}", 'A');
                android.Status = "BASE";
                android.Available = true;
                colonyAndroids.Add(android);
            }

            rover6 = new FieldUnit("ROVER-06", 'r');
            rover6.Status = "BASE";
            rover6.Available = true;

            currentDay++;
            ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
            lifeSupportDays--;
            basePower -= 10;

            UpdateWindTurbines(windTurbines, rand);

            double extensionWindPower = 0;

            foreach (WindTurbine turbine in windTurbines)
                extensionWindPower += turbine.CurrentPower;

            basePower += (int)Math.Round(extensionWindPower);
            basePower = Math.Min(100, Math.Max(0, basePower));

            colonyExtended = true;
            colonyExtensionUnlocked = false;

            ShowVictoryScreen(
                currentDay,
                totalCrew,
                windTurbines,
                fusionReactorsList,
                basaltMined,
                foodWarehouse,
                oxygenFactoryBuilt);

            return;
        }

        // =================================================
        // COLONY MAX LEVEL
        // =================================================

        Console.WriteLine();
        Console.WriteLine("Colony is already at MAX LEVEL.");
        Console.WriteLine("You are the COLONY GOVERNOR.");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // SHOW VICTORY SCREEN - NEW
    // =========================================================

    static void ShowVictoryScreen(
        int currentDay,
        int totalCrew,
        List<WindTurbine> windTurbines,
        List<FusionReactor> fusionReactors,
        int basaltMined,
        FoodWarehouse foodWarehouse,
        bool oxygenFactoryBuilt)
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine();
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║                                                              ║");
        Console.ForegroundColor = ConsoleColor.Yellow;

        // Blinking "CONGRATULATIONS"
        for (int blink = 0; blink < 3; blink++)
        {
            Console.SetCursorPosition(0, 3);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("║          ★★★  C O N G R A T U L A T I O N S  ★★★           ║");
            Thread.Sleep(400);

            Console.SetCursorPosition(0, 3);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("║                                                              ║");
            Thread.Sleep(400);
        }

        Console.SetCursorPosition(0, 3);
        Console.Write(new string(' ', 64));
        Console.SetCursorPosition(0, 3);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("║          ★★★  C O N G R A T U L A T I O N S  ★★★           ║");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║                                                              ║");

        // Blinking "COLONY GOVERNOR"
        for (int blink = 0; blink < 4; blink++)
        {
            Console.SetCursorPosition(0, 5);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("║          🏛️   Y O U   A R E   N O W   🏛️                     ║");
            Console.WriteLine("║                                                              ║");
            Console.WriteLine("║               ★  C O L O N Y   G O V E R N O R  ★            ║");
            Thread.Sleep(500);

            Console.SetCursorPosition(0, 5);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 6);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 7);
            Console.Write(new string(' ', 64));
            Thread.Sleep(300);
        }

        Console.SetCursorPosition(0, 5);
        Console.Write(new string(' ', 64));
        Console.SetCursorPosition(0, 6);
        Console.Write(new string(' ', 64));
        Console.SetCursorPosition(0, 7);
        Console.Write(new string(' ', 64));
        Console.SetCursorPosition(0, 5);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("║          🏛️   Y O U   A R E   N O W   🏛️                     ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║               ★  C O L O N Y   G O V E R N O R  ★            ║");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║                                                              ║");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("║                 APHRODITE COLONY IS COMPLETE                 ║");
        Console.WriteLine("║                                                              ║");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║          ───────────  STATISTICS  ───────────                ║");
        Console.WriteLine("║                                                              ║");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"║          - Days Survived     : {currentDay,-6}                    ║");
        Console.WriteLine($"║          - Total Personnel   : {totalCrew,-6}                    ║");
        Console.WriteLine($"║          - Wind Turbines     : {windTurbines.Count,-6}                    ║");
        Console.WriteLine($"║          - Fusion Reactors   : {fusionReactors.Count,-6}                    ║");
        Console.WriteLine($"║          - Basalt Stored     : {basaltMined,-6} Tons               ║");

        if (foodWarehouse != null)
        {
            Console.WriteLine($"║          - Food Stored       : {foodWarehouse.CurrentFoodTons,-6:F0} Tons               ║");
        }
        else
        {
            Console.WriteLine($"║          - Food Stored       : 0      Tons               ║");
        }

        if (oxygenFactoryBuilt)
        {
            Console.WriteLine($"║          - Oxygen Factory    : OPERATIONAL                   ║");
        }
        else
        {
            Console.WriteLine($"║          - Oxygen Factory    : NOT BUILT                     ║");
        }

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║                                                              ║");

        // Blinking "THE UNITED NATION COUNCIL THANKS YOU"
        for (int blink = 0; blink < 5; blink++)
        {
            Console.SetCursorPosition(0, 26);
            Console.Write(new string(' ', 64));
            Console.SetCursorPosition(0, 26);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║     THE UNITED NATION COUNCIL THANKS YOU FOR YOUR SERVICE!   ║");
            Thread.Sleep(500);

            Console.SetCursorPosition(0, 26);
            Console.Write(new string(' ', 64));
            Thread.Sleep(300);
        }

        Console.SetCursorPosition(0, 26);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("║     THE UNITED NATION COUNCIL THANKS YOU FOR YOUR SERVICE!   ║");

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("║                                                              ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
    
    // =========================================================
    // EXECUTE 3D BUILDER CONSTRUCTION (OPTION 9)
    // =========================================================

    static void Execute3DBuilderConstruction(
        ref char[,] grid,
        ref FieldUnit builder3D,
        ref int availableCrew,
        ref int assignedCrew,
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower)
    {
        if (builder3D != null)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: 3D Builder has already been constructed. Only 1 allowed.");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("            3D BUILDER CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The 3D Builder must be constructed on Rocky terrain (R).");
        Console.WriteLine();

        int bx;
        int by;

        while (true)
        {
            bx = AskCoordinate("X");
            by = AskCoordinate("Y");

            if (!IsInside(grid, bx, by))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[by, bx] != 'R')
            {
                Console.WriteLine("Please choose a rocky area (R).");
                continue;
            }

            break;
        }

        Console.WriteLine();
        Console.WriteLine("The 3D Builder requires no personnel.");
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Foundation preparation...");
        Thread.Sleep(3000);
        Console.WriteLine("[40%] Builder frame assembly...");
        Thread.Sleep(3000);
        Console.WriteLine("[60%] AI systems installation...");
        Thread.Sleep(3000);
        Console.WriteLine("[80%] Power systems connection...");
        Thread.Sleep(3000);
        Console.WriteLine("[100%] 3D Builder online...");
        Thread.Sleep(3000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[by, bx] = 'D';

        builder3D = new FieldUnit("3D BUILDER", 'D');
        builder3D.X = bx;
        builder3D.Y = by;
        builder3D.Status = "BASE";
        builder3D.Available = true;
        builder3D.Battery = 100;

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          3D BUILDER OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("3D Builder is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={bx}, Y={by}");
        Console.WriteLine("Battery: 100%");
        Console.WriteLine();
        Console.WriteLine("The 3D Builder can now construct:");
        Console.WriteLine("- Harp Machine (H)");
        Console.WriteLine("- Spaceport (P)");
        Console.WriteLine("- Oxygen Factory (O)");
        Console.WriteLine("- Fusion Reactor (R)");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // EXECUTE HARP MACHINE CONSTRUCTION (OPTION 10)
    // =========================================================

    static void ExecuteHarpMachineConstruction(
        ref char[,] grid,
        ref HarpMachine harpMachine,
        ref bool harpMachineBuilt,
        ref int availableCrew,
        ref int assignedCrew,
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        FieldUnit builder3D,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower)
    {
        if (harpMachineBuilt)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Harp Machine has already been constructed. Only 1 allowed.");
            Thread.Sleep(1500);
            return;
        }

        if (builder3D == null)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need a 3D Builder (D) to construct the Harp Machine.");
            Thread.Sleep(1500);
            return;
        }

        int availableAndroids = 0;
        foreach (FieldUnit android in colonyAndroids)
        {
            if (android != null && android.Available && android.Status == "BASE")
                availableAndroids++;
        }

        if (availableAndroids < 2)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need at least 2 available Androids.");
            Thread.Sleep(1500);
            return;
        }

        if (rover5 == null || !rover5.Available || rover5.Status != "BASE")
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need 1 available Rover (ROVER-05).");
            Thread.Sleep(1500);
            return;
        }

        if (availableCrew < 20)
        {
            Console.WriteLine();
            Console.WriteLine($"ERROR: You need at least 20 Personnel. Available: {availableCrew}");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          HARP MACHINE CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The Harp Machine can be constructed anywhere on open ground (.).");
        Console.WriteLine();

        int hx;
        int hy;

        while (true)
        {
            hx = AskCoordinate("X");
            hy = AskCoordinate("Y");

            if (!IsInside(grid, hx, hy))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[hy, hx] != '.' && grid[hy, hx] != 'R' && grid[hy, hx] != 'P')
            {
                Console.WriteLine("Please choose an open ground (. , R, or P).");
                continue;
            }

            break;
        }

        AssignColonyConstructionUnits(
            colonyAndroids, rover5, 2, 20,
            ref availableCrew, ref assignedCrew);

        Console.WriteLine();
        Console.WriteLine("Requirements met:");
        Console.WriteLine("- 2 Androids assigned");
        Console.WriteLine("- 1 Rover (ROVER-05) assigned");
        Console.WriteLine("- 20 Personnel assigned");
        Console.WriteLine("- 1 3D Builder (D)");
        Console.WriteLine();
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Foundation preparation...");
        Thread.Sleep(3000);
        Console.WriteLine("[40%] Harp frame assembly...");
        Thread.Sleep(3000);
        Console.WriteLine("[60%] Weather control systems...");
        Thread.Sleep(3000);
        Console.WriteLine("[80%] Atmospheric sensors installation...");
        Thread.Sleep(3000);
        Console.WriteLine("[100%] Harp Machine online...");
        Thread.Sleep(3000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[hy, hx] = 'H';

        harpMachine = new HarpMachine();
        harpMachine.X = hx;
        harpMachine.Y = hy;

        harpMachineBuilt = true;

        ReleaseColonyConstructionUnits(colonyAndroids, rover5, 20, ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          HARP MACHINE OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Harp Machine is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={hx}, Y={hy}");
        Console.WriteLine("Purpose: Weather Control");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // EXECUTE SPACEPORT CONSTRUCTION (OPTION 11)
    // =========================================================

    static void ExecuteSpaceportConstruction(
        ref char[,] grid,
        ref Spaceport spaceport,
        ref bool spaceportBuilt,
        ref int availableCrew,
        ref int assignedCrew,
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        FieldUnit builder3D,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower)
    {
        if (spaceportBuilt)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Spaceport has already been constructed. Only 1 allowed.");
            Thread.Sleep(1500);
            return;
        }

        if (builder3D == null)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need a 3D Builder (D) to construct the Spaceport.");
            Thread.Sleep(1500);
            return;
        }

        int availableAndroids = 0;
        foreach (FieldUnit android in colonyAndroids)
        {
            if (android != null && android.Available && android.Status == "BASE")
                availableAndroids++;
        }

        if (availableAndroids < 2)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need at least 2 available Androids.");
            Thread.Sleep(1500);
            return;
        }

        if (rover5 == null || !rover5.Available || rover5.Status != "BASE")
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need 1 available Rover (ROVER-05).");
            Thread.Sleep(1500);
            return;
        }

        if (availableCrew < 20)
        {
            Console.WriteLine();
            Console.WriteLine($"ERROR: You need at least 20 Personnel. Available: {availableCrew}");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("            SPACEPORT CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The Spaceport can be constructed on open ground (.).");
        Console.WriteLine();

        int px;
        int py;

        while (true)
        {
            px = AskCoordinate("X");
            py = AskCoordinate("Y");

            if (!IsInside(grid, px, py))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[py, px] != '.' && grid[py, px] != 'R' && grid[py, px] != 'P')
            {
                Console.WriteLine("Please choose an open ground (. , R, or P).");
                continue;
            }

            break;
        }

        AssignColonyConstructionUnits(
            colonyAndroids, rover5, 2, 20,
            ref availableCrew, ref assignedCrew);

        Console.WriteLine();
        Console.WriteLine("Requirements met:");
        Console.WriteLine("- 2 Androids assigned");
        Console.WriteLine("- 1 Rover (ROVER-05) assigned");
        Console.WriteLine("- 20 Personnel assigned");
        Console.WriteLine("- 1 3D Builder (D)");
        Console.WriteLine();
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Landing pad foundation...");
        Thread.Sleep(3000);
        Console.WriteLine("[40%] Launch tower assembly...");
        Thread.Sleep(3000);
        Console.WriteLine("[60%] Fuel systems installation...");
        Thread.Sleep(3000);
        Console.WriteLine("[80%] Control tower construction...");
        Thread.Sleep(3000);
        Console.WriteLine("[100%] Spaceport online...");
        Thread.Sleep(3000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[py, px] = 'P';

        spaceport = new Spaceport();
        spaceport.X = px;
        spaceport.Y = py;

        spaceportBuilt = true;

        ReleaseColonyConstructionUnits(colonyAndroids, rover5, 20, ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("            SPACEPORT OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Spaceport is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={px}, Y={py}");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // EXECUTE OXYGEN FACTORY CONSTRUCTION (OPTION 12)
    // =========================================================

    static void ExecuteOxygenFactoryConstruction(
        ref char[,] grid,
        ref OxygenFactory oxygenFactory,
        ref bool oxygenFactoryBuilt,
        ref int availableCrew,
        ref int assignedCrew,
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        FieldUnit builder3D,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower)
    {
        if (oxygenFactoryBuilt)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Oxygen Factory has already been constructed. Only 1 allowed.");
            Thread.Sleep(1500);
            return;
        }

        if (builder3D == null)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need a 3D Builder (D) to construct the Oxygen Factory.");
            Thread.Sleep(1500);
            return;
        }

        int availableAndroids = 0;
        foreach (FieldUnit android in colonyAndroids)
        {
            if (android != null && android.Available && android.Status == "BASE")
                availableAndroids++;
        }

        if (availableAndroids < 2)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need at least 2 available Androids.");
            Thread.Sleep(1500);
            return;
        }

        if (rover5 == null || !rover5.Available || rover5.Status != "BASE")
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need 1 available Rover (ROVER-05).");
            Thread.Sleep(1500);
            return;
        }

        if (availableCrew < 20)
        {
            Console.WriteLine();
            Console.WriteLine($"ERROR: You need at least 20 Personnel. Available: {availableCrew}");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          OXYGEN FACTORY CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The Oxygen Factory must be constructed on Rocky terrain (R).");
        Console.WriteLine();

        int ox;
        int oy;

        while (true)
        {
            ox = AskCoordinate("X");
            oy = AskCoordinate("Y");

            if (!IsInside(grid, ox, oy))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[oy, ox] != 'R')
            {
                Console.WriteLine("Please choose a rocky area (R).");
                continue;
            }

            break;
        }

        AssignColonyConstructionUnits(
            colonyAndroids, rover5, 2, 20,
            ref availableCrew, ref assignedCrew);

        Console.WriteLine();
        Console.WriteLine("Requirements met:");
        Console.WriteLine("- 2 Androids assigned");
        Console.WriteLine("- 1 Rover (ROVER-05) assigned");
        Console.WriteLine("- 20 Personnel assigned");
        Console.WriteLine("- 1 3D Builder (D)");
        Console.WriteLine();
        Console.WriteLine("Oxygen Capacity: 4000 m³");
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Foundation preparation...");
        Thread.Sleep(3000);
        Console.WriteLine("[40%] Electrolysis chamber construction...");
        Thread.Sleep(3000);
        Console.WriteLine("[60%] Storage tank installation...");
        Thread.Sleep(3000);
        Console.WriteLine("[80%] Atmospheric processing systems...");
        Thread.Sleep(3000);
        Console.WriteLine("[100%] Oxygen Factory online...");
        Thread.Sleep(3000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[oy, ox] = 'O';

        oxygenFactory = new OxygenFactory();
        oxygenFactory.X = ox;
        oxygenFactory.Y = oy;
        oxygenFactory.OxygenCubicMeters = 4000.0;

        oxygenFactoryBuilt = true;

        ReleaseColonyConstructionUnits(colonyAndroids, rover5, 20, ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          OXYGEN FACTORY OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Oxygen Factory is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={ox}, Y={oy}");
        Console.WriteLine($"Oxygen: {oxygenFactory.OxygenCubicMeters:F0} m³");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // EXECUTE FUSION REACTOR CONSTRUCTION (OPTION 13)
    // =========================================================

    static void ExecuteFusionReactorConstruction(
        ref char[,] grid,
        List<FusionReactor> fusionReactors,
        List<WindTurbine> windTurbines,
        ref int availableCrew,
        ref int assignedCrew,
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        FieldUnit builder3D,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower)
    {
        if (fusionReactors.Count >= 1)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Only 1 Fusion Reactor allowed.");
            Thread.Sleep(1500);
            return;
        }

        if (builder3D == null)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need a 3D Builder (D) to construct the Fusion Reactor.");
            Thread.Sleep(1500);
            return;
        }

        int availableAndroids = 0;
        foreach (FieldUnit android in colonyAndroids)
        {
            if (android != null && android.Available && android.Status == "BASE")
                availableAndroids++;
        }

        if (availableAndroids < 2)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need at least 2 available Androids.");
            Thread.Sleep(1500);
            return;
        }

        if (rover5 == null || !rover5.Available || rover5.Status != "BASE")
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: You need 1 available Rover (ROVER-05).");
            Thread.Sleep(1500);
            return;
        }

        if (availableCrew < 20)
        {
            Console.WriteLine();
            Console.WriteLine($"ERROR: You need at least 20 Personnel. Available: {availableCrew}");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          FUSION REACTOR CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The Fusion Reactor can be constructed on open ground (.).");
        Console.WriteLine();

        int rx;
        int ry;

        while (true)
        {
            rx = AskCoordinate("X");
            ry = AskCoordinate("Y");

            if (!IsInside(grid, rx, ry))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[ry, rx] != '.' && grid[ry, rx] != 'R' && grid[ry, rx] != 'P')
            {
                Console.WriteLine("Please choose an open ground (. , R, or P).");
                continue;
            }

            break;
        }

        AssignColonyConstructionUnits(
            colonyAndroids, rover5, 2, 20,
            ref availableCrew, ref assignedCrew);

        double powerOutput = windTurbines.Count * 0.5;

        Console.WriteLine();
        Console.WriteLine("Requirements met:");
        Console.WriteLine("- 2 Androids assigned");
        Console.WriteLine("- 1 Rover (ROVER-05) assigned");
        Console.WriteLine("- 20 Personnel assigned");
        Console.WriteLine("- 1 3D Builder (D)");
        Console.WriteLine();
        Console.WriteLine($"Power Output: {powerOutput:F1}% / day (50% of turbines)");
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Containment field construction...");
        Thread.Sleep(3000);
        Console.WriteLine("[40%] Magnetic coil installation...");
        Thread.Sleep(3000);
        Console.WriteLine("[60%] Plasma injection systems...");
        Thread.Sleep(3000);
        Console.WriteLine("[80%] Cooling systems installation...");
        Thread.Sleep(3000);
        Console.WriteLine("[100%] Fusion Reactor online...");
        Thread.Sleep(3000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[ry, rx] = 'R';

        FusionReactor reactor = new FusionReactor();
        reactor.X = rx;
        reactor.Y = ry;
        reactor.PowerPerDay = powerOutput;

        fusionReactors.Add(reactor);

        ReleaseColonyConstructionUnits(colonyAndroids, rover5, 20, ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          FUSION REACTOR OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Fusion Reactor is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={rx}, Y={ry}");
        Console.WriteLine($"Power Output: {reactor.PowerPerDay:F1}% / day");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // ASSIGN COLONY CONSTRUCTION UNITS
    // =========================================================

    static void AssignColonyConstructionUnits(
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        int androidsRequired,
        int personnelRequired,
        ref int availableCrew,
        ref int assignedCrew)
    {
        int assignedAndroids = 0;

        foreach (FieldUnit android in colonyAndroids)
        {
            if (android != null &&
                android.Available &&
                android.Status == "BASE" &&
                assignedAndroids < androidsRequired)
            {
                android.Available = false;
                android.Status = "CONSTRUCTING";
                assignedAndroids++;
            }
        }

        if (rover5 != null && rover5.Available && rover5.Status == "BASE")
        {
            rover5.Available = false;
            rover5.Status = "CONSTRUCTING";
        }

        availableCrew -= personnelRequired;
        assignedCrew += personnelRequired;
    }

    // =========================================================
    // RELEASE COLONY CONSTRUCTION UNITS
    // =========================================================

    static void ReleaseColonyConstructionUnits(
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        int personnelRequired,
        ref int availableCrew,
        ref int assignedCrew)
    {
        foreach (FieldUnit android in colonyAndroids)
        {
            if (android != null && android.Status == "CONSTRUCTING")
            {
                android.Status = "BASE";
                android.Available = true;
            }
        }

        if (rover5 != null && rover5.Status == "CONSTRUCTING")
        {
            rover5.Status = "BASE";
            rover5.Available = true;
        }

        availableCrew += personnelRequired;
        assignedCrew -= personnelRequired;
    }
    
    // =========================================================
    // EXECUTE FIELD OPERATION (OPTION 1)
    // =========================================================

    static void ExecuteFieldOperation(
        ref char[,] grid,
        int drillingX,
        int drillingY,
        ref int basaltX,
        ref int basaltY,
        ref bool basaltDiscovered,
        ref int basaltReserveTons,
        ref int basaltMined,
        ref bool ar1Available,
        ref bool ar2Available,
        ref int ar1Battery,
        ref int ar2Battery,
        ref int roverBattery,
        ref int ar1Condition,
        ref int ar2Condition,
        ref int roverCondition,
        FieldUnit ar1,
        FieldUnit ar2,
        FieldUnit rover,
        Random rand,
        int maxBasaltStorage,
        int fieldTickMilliseconds)
    {
        int availableSearchAndroids = 0;

        if (ar1Available && ar1Battery > 30)
            availableSearchAndroids++;

        if (ar2Available && ar2Battery > 30)
            availableSearchAndroids++;

        if (availableSearchAndroids == 0)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No Android has enough battery for field deployment.");
            Thread.Sleep(1800);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());

        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("              BASALT SEARCH OPERATION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Androids will search independently near the Lab.");
        Console.WriteLine("When one discovers basalt, the area will be marked B.");
        Console.WriteLine("The other Android will then converge on the same deposit.");
        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║  ⚠️  ROVER-01 is on STANDBY at the Laboratory.              ║");
        Console.WriteLine("║  Press 'R' AFTER basalt is discovered to deploy it.         ║");
        Console.WriteLine($"║  Each R press delivers 30 Tons to Storage (max {maxBasaltStorage})         ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.ResetColor();
        Console.WriteLine();

        int androidsToDeploy = AskNumber("How many Androids to deploy", 1, availableSearchAndroids);

        List<FieldUnit> searchUnits = new List<FieldUnit>();

        if (androidsToDeploy >= 1 && ar1Available && ar1Battery > 30)
        {
            ar1Available = false;
            ar1.Available = false;
            ar1.Status = "SEARCHING";
            ar1.Battery = ar1Battery;
            ar1.Condition = ar1Condition;
            searchUnits.Add(ar1);
        }

        if (searchUnits.Count < androidsToDeploy && ar2Available && ar2Battery > 30)
        {
            ar2Available = false;
            ar2.Available = false;
            ar2.Status = "SEARCHING";
            ar2.Battery = ar2Battery;
            ar2.Condition = ar2Condition;
            searchUnits.Add(ar2);
        }

        if (!basaltDiscovered)
        {
            FindHiddenBasalt(grid, drillingX, drillingY, ref basaltX, ref basaltY, rand);
        }

        bool fieldActive = true;
        bool roverDispatched = false;
        bool operationCompleted = false;

        while (fieldActive)
        {
            List<FieldUnit> currentUnits = new List<FieldUnit>();

            if (rover != null && rover.Status != "BASE" && rover.Status != "RETURNED")
                currentUnits.Add(rover);
            if (ar1 != null && ar1.Status != "BASE" && ar1.Status != "RETURNED")
                currentUnits.Add(ar1);
            if (ar2 != null && ar2.Status != "BASE" && ar2.Status != "RETURNED")
                currentUnits.Add(ar2);

            Console.Clear();
            DrawGrid(grid, currentUnits);

            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("             LIVE FIELD OPERATION");
            Console.WriteLine("==================================================");
            Console.WriteLine();

            foreach (FieldUnit unit in searchUnits)
            {
                Console.WriteLine($"{unit.Name} | X={unit.X}, Y={unit.Y} | Battery={unit.Battery}% | Status={unit.Status}");
            }

            if (roverDispatched)
            {
                Console.WriteLine($"{rover.Name} | X={rover.X}, Y={rover.Y} | Battery={rover.Battery}% | Status={rover.Status}");
            }

            Console.WriteLine();
            Console.WriteLine("5 game-minutes = 0.5 seconds in this prototype.");
            Console.WriteLine();
            Console.WriteLine("ENTER = advance 5 game-minutes");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("R = dispatch Rover after basalt discovery");
            Console.ResetColor();
            Console.WriteLine("Q = order all units to return to Lab");
            Console.WriteLine();

            ConsoleKeyInfo key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Q)
            {
                foreach (FieldUnit unit in searchUnits)
                {
                    if (unit.Status != "RETURNED")
                        unit.Status = "RETURNING";
                }

                if (roverDispatched)
                    rover.Status = "RETURNING";

                Console.WriteLine();
                Console.WriteLine("COMMANDER ORDER: ALL FIELD UNITS RETURN TO LAB.");
                Thread.Sleep(1000);
            }
            else if (key.Key == ConsoleKey.R && basaltDiscovered && !roverDispatched)
            {
                if (roverBattery <= 30)
                {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: Rover battery is too low for deployment.");
                    Thread.Sleep(1500);
                }
                else
                {
                    int currentBasalt = basaltMined;
                    int maxCollect = maxBasaltStorage - currentBasalt;
                    int load = Math.Min(30, maxCollect);

                    if (load > 0)
                    {
                        basaltMined += load;
                        roverDispatched = true;
                        rover.Available = false;
                        rover.Status = "TO BASALT";

                        Console.Clear();
                        DrawGrid(grid, currentUnits);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                        Console.WriteLine("║                                                              ║");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("║          📦  BASALT DELIVERED TO LABORATORY!  📦             ║");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("║                                                              ║");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"║     ROVER-01 delivered {load} Tons of Basalt to the Lab        ║");
                        Console.WriteLine("║                                                              ║");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"║        📊 Total Basalt Collected: {basaltMined} / {maxBasaltStorage} Tons    ║");
                        Console.WriteLine($"║        📦 Deposit Reserve: {basaltReserveTons} Tons           ║");
                        Console.WriteLine("║                                                              ║");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("║        💾 Basalt stored in Laboratory storage                ║");

                        if (basaltMined >= maxBasaltStorage)
                        {
                            Console.WriteLine("║        🎉 LABORATORY STORAGE IS NOW FULL!                    ║");
                            Console.WriteLine($"║        ⚡ You have collected ALL {maxBasaltStorage} Tons of Basalt!         ║");
                        }
                        else
                        {
                            Console.WriteLine("║        🔄 Press 'R' again to collect 30 more Tons            ║");
                            Console.WriteLine($"║        📦 {maxBasaltStorage - basaltMined} Tons remaining to collect        ║");
                        }

                        Console.WriteLine("║                                                              ║");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"⚠️ Laboratory storage is FULL! ({maxBasaltStorage} Tons collected)");
                        Console.ResetColor();
                        Thread.Sleep(1500);
                    }
                }
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                Console.WriteLine("Advancing 5 game-minutes...");
                Thread.Sleep(fieldTickMilliseconds);

                foreach (FieldUnit unit in searchUnits)
                {
                    if (unit.Status != "RETURNED" && unit.Status != "BASE")
                    {
                        unit.Battery = Math.Max(0, unit.Battery - 25);
                        unit.Condition = Math.Max(0, unit.Condition - 1);
                    }
                }

                if (roverDispatched && rover.Status != "RETURNED" && rover.Status != "BASE")
                {
                    rover.Battery = Math.Max(0, rover.Battery - 25);
                    rover.Condition = Math.Max(0, rover.Condition - 1);
                }

                foreach (FieldUnit unit in searchUnits)
                {
                    if (unit.Status == "SEARCHING")
                    {
                        if (!basaltDiscovered)
                        {
                            List<Point> path = FindPath(grid, unit.X, unit.Y, basaltX, basaltY);

                            if (path.Count > 0)
                            {
                                Point next = path[0];
                                unit.X = next.X;
                                unit.Y = next.Y;
                            }

                            if (unit.X == basaltX && unit.Y == basaltY)
                            {
                                basaltDiscovered = true;
                                basaltReserveTons = 50000;
                                grid[basaltY, basaltX] = 'B';

                                Console.Clear();
                                DrawGrid(grid, currentUnits);
                                Console.WriteLine();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("║          ★★★  BASALT DEPOSIT DISCOVERED!  ★★★                ║");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"║     {unit.Name} found a basalt deposit at location:            ║");
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine($"║        📍 X = {basaltX} , Y = {basaltY}                         ║");
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"║        📦 Reserve: {basaltReserveTons} Tons (deposit)          ║");
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("║        💡 Area marked with B on the Grid                     ║");
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("║        🚀 ROVER-01 is on STANDBY                             ║");
                                Console.WriteLine("║        Press 'R' to collect 30 Tons of Basalt                ║");
                                Console.WriteLine($"║        (Storage capacity: {maxBasaltStorage} Tons)                          ║");
                                Console.WriteLine("║                                                              ║");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
                                Console.ResetColor();
                                Console.WriteLine();
                                Console.WriteLine("Press any key to continue...");
                                Console.ReadKey();
                            }
                        }
                        else
                        {
                            unit.Status = "CONVERGING";
                        }
                    }

                    if (unit.Status == "CONVERGING")
                    {
                        List<Point> path = FindPath(grid, unit.X, unit.Y, basaltX, basaltY);

                        if (path.Count > 0)
                        {
                            Point next = path[0];
                            unit.X = next.X;
                            unit.Y = next.Y;
                        }

                        if (unit.X == basaltX && unit.Y == basaltY)
                        {
                            unit.Status = "AT BASALT";
                        }
                    }
                }

                foreach (FieldUnit unit in searchUnits)
                {
                    if (unit.Battery <= 30 && unit.Status != "RETURNED")
                    {
                        unit.Status = "RETURNING";
                    }
                }

                if (roverDispatched && rover.Battery <= 30 && rover.Status != "RETURNED")
                {
                    rover.Status = "RETURNING";
                }

                if (roverDispatched)
                {
                    if (rover.Status == "TO BASALT")
                    {
                        List<Point> pathToBasalt = FindPath(grid, rover.X, rover.Y, basaltX, basaltY);

                        if (pathToBasalt.Count > 0)
                        {
                            Point next = pathToBasalt[0];
                            rover.X = next.X;
                            rover.Y = next.Y;
                        }

                        if (rover.X == basaltX && rover.Y == basaltY)
                        {
                            rover.Status = "TO LAB";
                        }
                    }
                    else if (rover.Status == "TO LAB" || rover.Status == "RETURNING")
                    {
                        List<Point> pathToLab = FindPath(grid, rover.X, rover.Y, drillingX, drillingY);

                        if (pathToLab.Count > 0)
                        {
                            Point next = pathToLab[0];
                            rover.X = next.X;
                            rover.Y = next.Y;
                        }

                        if (rover.X == drillingX && rover.Y == drillingY)
                        {
                            rover.Status = "RETURNED";
                            rover.Available = true;
                        }
                    }
                }

                foreach (FieldUnit unit in searchUnits)
                {
                    if (unit.Status == "RETURNING")
                    {
                        List<Point> pathToLab = FindPath(grid, unit.X, unit.Y, drillingX, drillingY);

                        if (pathToLab.Count > 0)
                        {
                            Point next = pathToLab[0];
                            unit.X = next.X;
                            unit.Y = next.Y;
                        }

                        if (unit.X == drillingX && unit.Y == drillingY)
                        {
                            unit.Status = "RETURNED";
                            unit.Available = true;
                        }
                    }
                }

                bool allAndroidsReturned = true;

                foreach (FieldUnit unit in searchUnits)
                {
                    if (unit.Status != "RETURNED")
                        allAndroidsReturned = false;
                }

                bool roverDone = !roverDispatched || rover.Status == "RETURNED";

                if (allAndroidsReturned && roverDone)
                {
                    fieldActive = false;
                    operationCompleted = true;
                }
            }
        }

        ar1Battery = ar1.Battery;
        ar1Condition = ar1.Condition;
        ar2Battery = ar2.Battery;
        ar2Condition = ar2.Condition;
        roverBattery = rover.Battery;
        roverCondition = rover.Condition;

        if (ar1.Status == "RETURNED" || ar1.Status == "BASE")
        {
            ar1Available = true;
            ar1.Status = "BASE";
        }

        if (ar2.Status == "RETURNED" || ar2.Status == "BASE")
        {
            ar2Available = true;
            ar2.Status = "BASE";
        }

        if (rover.Status == "RETURNED" || rover.Status == "BASE")
        {
            rover.Status = "BASE";
        }

        if (operationCompleted)
        {
            Console.Clear();
            DrawGrid(grid, new List<FieldUnit>());
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("           FIELD OPERATION COMPLETE");
            Console.WriteLine("==================================================");
            Console.WriteLine();
            Console.WriteLine($"Basalt collected: {basaltMined} Tons");
            Console.WriteLine();
            Console.WriteLine("All deployed units have returned to the Laboratory.");
            Console.WriteLine();
            Console.WriteLine("IMPORTANT:");
            Console.WriteLine("L, B, W, C remain permanently on the Grid.");
            Console.ReadKey();
        }
    }

    // =========================================================
    // EXECUTE WIND TURBINE CONSTRUCTION (OPTION 2)
    // =========================================================

    static void ExecuteWindTurbineConstruction(
        ref char[,] grid,
        int drillingX,
        int drillingY,
        List<WindTurbine> windTurbines,
        Random rand,
        ref int currentDay,
        ref int basePower,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int availableCrew,
        ref int assignedCrew,
        ref bool ar1Available,
        ref bool ar2Available,
        ref int ar1Battery,
        ref int ar2Battery,
        ref int ar1Condition,
        ref int ar2Condition)
    {
        int availableAndroids = 0;

        if (ar1Available) availableAndroids++;
        if (ar2Available) availableAndroids++;

        if (availableAndroids == 0)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No Android units are currently available.");
            Thread.Sleep(1500);
            return;
        }

        if (availableCrew == 0)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No personnel are currently available.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("       WIND TURBINE CONSTRUCTION");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine($"Available Androids: {availableAndroids}");

        int androidsRequired = AskNumber("How many Androids for construction", 1, availableAndroids);
        int personnelRequired = AskNumber("How many personnel for construction", 1, availableCrew);

        bool batteryRequirementOK = true;

        if (androidsRequired == 1)
        {
            if (!(ar1Available && ar1Battery >= 20) && !(ar2Available && ar2Battery >= 20))
                batteryRequirementOK = false;
        }

        if (androidsRequired == 2)
        {
            if (!ar1Available || !ar2Available || ar1Battery < 20 || ar2Battery < 20)
                batteryRequirementOK = false;
        }

        if (!batteryRequirementOK)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Selected Android units do not have enough battery.");
            Thread.Sleep(2000);
            return;
        }

        int wtX = -1;
        int wtY = -1;

        int[,] turbineOffsets = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        for (int i = 0; i < turbineOffsets.GetLength(0); i++)
        {
            int checkY = drillingY + turbineOffsets[i, 0];
            int checkX = drillingX + turbineOffsets[i, 1];

            if (IsInside(grid, checkX, checkY) && grid[checkY, checkX] == '.')
            {
                wtY = checkY;
                wtX = checkX;
                break;
            }
        }

        if (wtX == -1)
        {
            for (int r = drillingY - 2; r <= drillingY + 2; r++)
            {
                for (int c = drillingX - 2; c <= drillingX + 2; c++)
                {
                    if (IsInside(grid, c, r) &&
                        Math.Abs(r - drillingY) + Math.Abs(c - drillingX) == 2 &&
                        grid[r, c] == '.')
                    {
                        wtY = r;
                        wtX = c;
                        break;
                    }
                }
                if (wtX != -1)
                    break;
            }
        }

        if (wtX == -1)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No suitable open ground near Lab.");
            Thread.Sleep(2000);
            return;
        }

        bool ar1Construction = false;
        bool ar2Construction = false;

        if (androidsRequired >= 1)
        {
            if (ar1Available && ar1Battery >= 20)
                ar1Construction = true;
            else if (ar2Available && ar2Battery >= 20)
                ar2Construction = true;
        }

        if (androidsRequired == 2)
        {
            if (ar1Available && ar2Available && ar1Battery >= 20 && ar2Battery >= 20)
            {
                ar1Construction = true;
                ar2Construction = true;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("ERROR: Two suitable Androids are not available.");
                Thread.Sleep(2000);
                return;
            }
        }

        if (ar1Construction)
        {
            ar1Available = false;
            ar1Battery = Math.Max(0, ar1Battery - 20);
            ar1Condition = Math.Max(0, ar1Condition - 1);
        }

        if (ar2Construction)
        {
            ar2Available = false;
            ar2Battery = Math.Max(0, ar2Battery - 20);
            ar2Condition = Math.Max(0, ar2Condition - 1);
        }

        availableCrew -= personnelRequired;
        assignedCrew += personnelRequired;

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("       WIND TURBINE CONSTRUCTION");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine($"Construction Site: X={wtX}, Y={wtY}");
        Console.WriteLine();
        Console.WriteLine($"Androids assigned : {androidsRequired}");
        Console.WriteLine($"Personnel assigned: {personnelRequired}");
        Console.WriteLine();
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Foundation assembly...");
        Thread.Sleep(4000);
        Console.WriteLine("[40%] Tower installation...");
        Thread.Sleep(4000);
        Console.WriteLine("[60%] Generator installation...");
        Thread.Sleep(4000);
        Console.WriteLine("[80%] Electrical connection...");
        Thread.Sleep(4000);
        Console.WriteLine("[100%] Final systems check...");
        Thread.Sleep(4000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        UpdateWindTurbines(windTurbines, rand);

        double constructionDayWindPower = 0;

        foreach (WindTurbine turbine in windTurbines)
            constructionDayWindPower += turbine.CurrentPower;

        basePower += (int)Math.Round(constructionDayWindPower);

        ar1Battery = Math.Min(100, ar1Battery + 40);
        ar2Battery = Math.Min(100, ar2Battery + 40);

        WindTurbine newTurbine = new WindTurbine();
        newTurbine.X = wtX;
        newTurbine.Y = wtY;
        newTurbine.WindDynamics = rand.Next(40, 81);
        newTurbine.CurrentPower = newTurbine.MaxPowerPerHour *
            (newTurbine.WindDynamics / 100.0) *
            (newTurbine.Condition / 100.0);

        grid[wtY, wtX] = 'W';
        windTurbines.Add(newTurbine);

        availableCrew += personnelRequired;
        assignedCrew -= personnelRequired;

        if (ar1Construction) ar1Available = true;
        if (ar2Construction) ar2Available = true;

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==============================================");
        Console.WriteLine("       WIND TURBINE CONSTRUCTION COMPLETE");
        Console.WriteLine("==============================================");
        Console.WriteLine();
        Console.WriteLine($"W-{windTurbines.Count:00} is now operational.");
        Console.WriteLine();
        Console.WriteLine($"Location: X={wtX}, Y={wtY}");
        Console.WriteLine($"Wind Dynamics: {newTurbine.WindDynamics}%");
        Console.WriteLine($"Initial Output: {newTurbine.CurrentPower:F1}/hour");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Thread.Sleep(3000);
    }

    // =========================================================
    // EXECUTE RESUPPLY (OPTION 4)
    // =========================================================

    static void ExecuteResupply(
        ref bool resupplyRequested,
        ref int resupplyEtaDays)
    {
        if (!resupplyRequested)
        {
            resupplyRequested = true;
            resupplyEtaDays = 3;
            Console.WriteLine();
            Console.WriteLine("EMERGENCY TRANSMISSION SENT!");
            Console.WriteLine("Resupply Pod ETA: 3 Days.");
        }
        else
        {
            Console.WriteLine();
            Console.WriteLine($"Resupply Pod already in transit! ETA: {resupplyEtaDays} Days.");
        }

        Thread.Sleep(1500);
    }

    // =========================================================
    // EXECUTE END DAY (OPTION 5) - UPDATED με Colony Extension
    // =========================================================

    static void ExecuteEndDay(
        ref int currentDay,
        ref int basePower,
        ref int foodRationsDays,
        ref int lifeSupportDays,
        ref double foodConsumptionAccumulator,
        ref int ar1Battery,
        ref int ar2Battery,
        ref int roverBattery,
        FieldUnit ar3,
        FieldUnit ar4,
        FieldUnit rover2,
        FieldUnit ar5,
        FieldUnit ar6,
        FieldUnit ar7,
        FieldUnit ar8,
        FieldUnit ar9,
        FieldUnit rover3,
        FieldUnit rover4,
        List<FieldUnit> colonyAndroids,
        FieldUnit rover5,
        FieldUnit rover6,
        FieldUnit builder3D,
        List<WindTurbine> windTurbines,
        List<FusionReactor> fusionReactors,
        Random rand,
        double hydroponizedProductionKg,
        ref bool resupplyRequested,
        ref int resupplyEtaDays,
        ref bool missionActive,
        bool laboratoryExpanded,
        bool baseCreated,
        bool baseExpanded,
        bool colonyExpanded,
        bool colonyExtended)
    {
        currentDay++;

        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, hydroponizedProductionKg);
        lifeSupportDays--;
        basePower -= 10;

        UpdateWindTurbines(windTurbines, rand);

        double totalWindPower = 0;

        foreach (WindTurbine turbine in windTurbines)
            totalWindPower += turbine.CurrentPower;

        double totalFusionPower = 0;

        foreach (FusionReactor reactor in fusionReactors)
        {
            totalFusionPower += reactor.PowerPerDay;

            reactor.Condition = Math.Max(0, reactor.Condition - 1);
            reactor.PowerPerDay = windTurbines.Count * 0.5 *
                (reactor.Condition / 100.0);
        }

        basePower += (int)Math.Round(totalWindPower + totalFusionPower);
        basePower = Math.Min(100, Math.Max(0, basePower));

        ar1Battery = Math.Min(100, ar1Battery + 40);
        ar2Battery = Math.Min(100, ar2Battery + 40);
        roverBattery = Math.Min(100, roverBattery + 50);

        if (baseCreated && ar3 != null)
            ar3.Battery = Math.Min(100, ar3.Battery + 40);
        if (baseCreated && ar4 != null)
            ar4.Battery = Math.Min(100, ar4.Battery + 40);
        if (baseCreated && rover2 != null)
            rover2.Battery = Math.Min(100, rover2.Battery + 50);

        if (baseExpanded && ar5 != null) ar5.Battery = Math.Min(100, ar5.Battery + 40);
        if (baseExpanded && ar6 != null) ar6.Battery = Math.Min(100, ar6.Battery + 40);
        if (baseExpanded && ar7 != null) ar7.Battery = Math.Min(100, ar7.Battery + 40);
        if (baseExpanded && ar8 != null) ar8.Battery = Math.Min(100, ar8.Battery + 40);
        if (baseExpanded && ar9 != null) ar9.Battery = Math.Min(100, ar9.Battery + 40);
        if (baseExpanded && rover3 != null) rover3.Battery = Math.Min(100, rover3.Battery + 50);
        if (baseExpanded && rover4 != null) rover4.Battery = Math.Min(100, rover4.Battery + 50);

        if (colonyExpanded)
        {
            foreach (FieldUnit android in colonyAndroids)
            {
                if (android != null)
                    android.Battery = Math.Min(100, android.Battery + 40);
            }

            if (rover5 != null)
                rover5.Battery = Math.Min(100, rover5.Battery + 50);

            if (builder3D != null)
                builder3D.Battery = Math.Min(100, builder3D.Battery + 40);
        }

        if (colonyExtended && rover6 != null)
            rover6.Battery = Math.Min(100, rover6.Battery + 50);

        if (resupplyRequested)
        {
            resupplyEtaDays--;

            if (resupplyEtaDays <= 0)
            {
                resupplyRequested = false;
                foodRationsDays += 7;
                lifeSupportDays += 7;
                basePower = 100;

                Console.WriteLine();
                Console.WriteLine("==================================================");
                Console.WriteLine("RESUPPLY POD LANDED!");
                Console.WriteLine("Food, Life Support & Power Restored!");
                Console.WriteLine("==================================================");
                Thread.Sleep(2500);
            }
        }

        if (foodRationsDays <= 0 || lifeSupportDays <= 0 || basePower <= 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("==================================================");
            Console.WriteLine("             MISSION FAILED: BASE COLLAPSE");
            Console.WriteLine("==================================================");

            if (foodRationsDays <= 0)
                Console.WriteLine("Reason: Crew ran out of food rations.");

            if (lifeSupportDays <= 0)
                Console.WriteLine("Reason: Life support system failed.");

            if (basePower <= 0)
                Console.WriteLine("Reason: Total power grid failure.");

            Console.WriteLine("==================================================");
            Console.ResetColor();
            missionActive = false;
            Console.ReadKey();
        }
    }

    // =========================================================
    // EXECUTE FARM CONSTRUCTION (OPTION 7)
    // =========================================================

    static void ExecuteFarmConstruction(
        ref char[,] grid,
        List<HydroponizedFarm> farms,
        ref int basaltMined,
        ref int availableCrew,
        ref int assignedCrew,
        FieldUnit ar3,
        FieldUnit ar4,
        FieldUnit ar5,
        FieldUnit ar6,
        FieldUnit ar7,
        FieldUnit ar8,
        FieldUnit ar9,
        FieldUnit rover2,
        FieldUnit rover3,
        FieldUnit rover4,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower,
        int maxBasaltStorage)
    {
        if (farms.Count >= 2)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Maximum of 2 Hydroponized Farms already constructed.");
            Thread.Sleep(1500);
            return;
        }

        if (basaltMined < 300)
        {
            Console.WriteLine();
            Console.WriteLine($"ERROR: A Hydroponized Farm requires 300 Tons of Basalt.");
            Console.WriteLine($"Current Basalt: {basaltMined} Tons.");
            Thread.Sleep(1800);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          HYDROPONIZED FARM CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The Farm must be constructed on Rocky terrain (R).");
        Console.WriteLine();

        int farmX;
        int farmY;

        while (true)
        {
            farmX = AskCoordinate("X");
            farmY = AskCoordinate("Y");

            if (!IsInside(grid, farmX, farmY))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[farmY, farmX] != 'R')
            {
                Console.WriteLine("Please choose a rocky area (R).");
                continue;
            }

            break;
        }

        if (!CanUseConstructionUnits(ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
                out int availableAndroids, out int availableRovers))
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No suitable construction units are available.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Available Androids: {availableAndroids}");
        int androidsRequired = AskNumber("How many Androids for construction", 1, availableAndroids);

        Console.WriteLine();
        Console.WriteLine($"Available Rovers: {availableRovers}");
        int roversRequired = AskNumber("How many Rovers for construction", 1, availableRovers);

        if (availableCrew <= 0)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No personnel available.");
            Thread.Sleep(1500);
            return;
        }

        int personnelRequired = AskNumber("How many personnel for construction", 1, availableCrew);

        AssignConstructionUnits(androidsRequired, roversRequired, personnelRequired,
            availableCrew, ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
            ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("       HYDROPONIZED FARM CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine($"Construction Site: X={farmX}, Y={farmY}");
        Console.WriteLine();
        Console.WriteLine($"Androids assigned : {androidsRequired}");
        Console.WriteLine($"Rovers assigned   : {roversRequired}");
        Console.WriteLine($"Personnel assigned: {personnelRequired}");
        Console.WriteLine();
        Console.WriteLine("Basalt required: 300 Tons");
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Foundation preparation...");
        Thread.Sleep(4000);
        Console.WriteLine("[40%] Hydroponic chamber assembly...");
        Thread.Sleep(4000);
        Console.WriteLine("[60%] Irrigation and nutrient systems...");
        Thread.Sleep(4000);
        Console.WriteLine("[80%] Environmental control systems...");
        Thread.Sleep(4000);
        Console.WriteLine("[100%] Production systems online...");
        Thread.Sleep(4000);

        basaltMined -= 300;
        currentDay++;

        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[farmY, farmX] = 'H';

        HydroponizedFarm farm = new HydroponizedFarm();
        farm.X = farmX;
        farm.Y = farmY;
        farm.LastProductionTime = DateTime.Now;
        farms.Add(farm);

        ReleaseConstructionUnits(androidsRequired, roversRequired, personnelRequired,
            ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
            ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("       HYDROPONIZED FARM OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Hydroponized Farm H-{farms.Count:00} is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={farmX}, Y={farmY}");
        Console.WriteLine("Production: +10 Tons every 30 seconds");
        Console.WriteLine();
        Console.WriteLine($"Total Farms: {farms.Count}/2");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // EXECUTE WAREHOUSE CONSTRUCTION (OPTION 8)
    // =========================================================

    static void ExecuteWarehouseConstruction(
        ref char[,] grid,
        ref FoodWarehouse warehouse,
        ref int availableCrew,
        ref int assignedCrew,
        FieldUnit ar3,
        FieldUnit ar4,
        FieldUnit ar5,
        FieldUnit ar6,
        FieldUnit ar7,
        FieldUnit ar8,
        FieldUnit ar9,
        FieldUnit rover2,
        FieldUnit rover3,
        FieldUnit rover4,
        ref int currentDay,
        ref int foodRationsDays,
        ref double foodConsumptionAccumulator,
        ref int lifeSupportDays,
        ref int basePower)
    {
        if (warehouse != null)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: Food Warehouse has already been constructed.");
            Thread.Sleep(1500);
            return;
        }

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("             FOOD WAREHOUSE CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("Commander, select the construction coordinates.");
        Console.WriteLine("The Warehouse must be constructed on Rocky terrain (R).");
        Console.WriteLine();

        int warehouseX;
        int warehouseY;

        while (true)
        {
            warehouseX = AskCoordinate("X");
            warehouseY = AskCoordinate("Y");

            if (!IsInside(grid, warehouseX, warehouseY))
            {
                Console.WriteLine("Invalid coordinates. Please choose a position between 0 and 29.");
                continue;
            }

            if (grid[warehouseY, warehouseX] != 'R')
            {
                Console.WriteLine("Please choose a rocky area (R).");
                continue;
            }

            break;
        }

        if (!CanUseConstructionUnits(ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
                out int availableAndroids, out int availableRovers))
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No suitable construction units are available.");
            Thread.Sleep(1500);
            return;
        }

        Console.WriteLine();
        Console.WriteLine($"Available Androids: {availableAndroids}");
        int androidsRequired = AskNumber("How many Androids for construction", 1, availableAndroids);

        Console.WriteLine();
        Console.WriteLine($"Available Rovers: {availableRovers}");
        int roversRequired = AskNumber("How many Rovers for construction", 1, availableRovers);

        if (availableCrew <= 0)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: No personnel available.");
            Thread.Sleep(1500);
            return;
        }

        int personnelRequired = AskNumber("How many personnel for construction", 1, availableCrew);

        AssignConstructionUnits(androidsRequired, roversRequired, personnelRequired,
            availableCrew, ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
            ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          FOOD WAREHOUSE CONSTRUCTION");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine($"Construction Site: X={warehouseX}, Y={warehouseY}");
        Console.WriteLine();
        Console.WriteLine($"Androids assigned : {androidsRequired}");
        Console.WriteLine($"Rovers assigned   : {roversRequired}");
        Console.WriteLine($"Personnel assigned: {personnelRequired}");
        Console.WriteLine();
        Console.WriteLine("Warehouse Capacity: 30 Tons");
        Console.WriteLine("Production: +10 Tons / 30 sec");
        Console.WriteLine("Consumption: -2 Tons / 15 sec");
        Console.WriteLine("Construction Time: 1 Day");
        Console.WriteLine();
        Console.WriteLine("[20%] Foundation preparation...");
        Thread.Sleep(4000);
        Console.WriteLine("[40%] Storage chamber construction...");
        Thread.Sleep(4000);
        Console.WriteLine("[60%] Environmental control installation...");
        Thread.Sleep(4000);
        Console.WriteLine("[80%] Food preservation systems...");
        Thread.Sleep(4000);
        Console.WriteLine("[100%] Warehouse systems online...");
        Thread.Sleep(4000);

        currentDay++;
        ConsumeFoodForDay(ref foodRationsDays, ref foodConsumptionAccumulator, 0);
        lifeSupportDays--;
        basePower -= 10;

        grid[warehouseY, warehouseX] = 'F';

        warehouse = new FoodWarehouse();
        warehouse.X = warehouseX;
        warehouse.Y = warehouseY;
        warehouse.LastConsumptionTime = DateTime.Now;

        ReleaseConstructionUnits(androidsRequired, roversRequired, personnelRequired,
            ar3, ar4, ar5, ar6, ar7, ar8, ar9, rover2, rover3, rover4,
            ref availableCrew, ref assignedCrew);

        Console.Clear();
        DrawGrid(grid, new List<FieldUnit>());
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine("          FOOD WAREHOUSE OPERATIONAL");
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Food Warehouse is now operational.");
        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine($"Location: X={warehouseX}, Y={warehouseY}");
        Console.WriteLine("Capacity: 30 Tons");
        Console.WriteLine();
        Console.WriteLine($"MISSION DAY ADVANCED TO DAY {currentDay}");
        Console.WriteLine();
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    // =========================================================
    // CONSUME FOOD BASED ON HYDROPONIZED PRODUCTION
    // =========================================================

    static void ConsumeFoodForDay(
        ref int foodRationsDays,
        ref double foodAccumulator,
        double hydroKg)
    {
        double rate = Math.Max(0.0, 1.0 - (hydroKg / 250.0));

        foodAccumulator += rate;

        int wholeDays = (int)foodAccumulator;

        if (wholeDays > 0)
        {
            foodRationsDays -= wholeDays;
            foodAccumulator -= wholeDays;
        }
    }

    // =========================================================
    // UPDATE HYDROPONIZED PRODUCTION
    // =========================================================

    static void UpdateHydroponizedProduction(
        ref double productionKg,
        bool productionStarted,
        DateTime productionStartTime,
        double maximumKg,
        double productionDurationSeconds)
    {
        if (!productionStarted)
            return;

        if (productionKg >= maximumKg)
        {
            productionKg = maximumKg;
            return;
        }

        double elapsedSeconds = (DateTime.Now - productionStartTime).TotalSeconds;

        if (elapsedSeconds <= 0)
            return;

        double progress = elapsedSeconds / productionDurationSeconds;
        progress = Math.Min(1.0, progress);
        productionKg = maximumKg * progress;

        if (productionKg > maximumKg)
            productionKg = maximumKg;
    }

    // =========================================================
    // UPDATE FARM / WAREHOUSE FOOD SYSTEM
    // =========================================================

    static void UpdateBaseFoodSystem(
        List<HydroponizedFarm> farms,
        FoodWarehouse warehouse)
    {
        if (warehouse == null || !warehouse.Operational)
            return;

        DateTime now = DateTime.Now;

        double consumptionElapsed = (now - warehouse.LastConsumptionTime).TotalSeconds;

        if (consumptionElapsed >= 15.0)
        {
            int consumptionCycles = (int)(consumptionElapsed / 15.0);

            warehouse.LastConsumptionTime =
                warehouse.LastConsumptionTime.AddSeconds(consumptionCycles * 15.0);

            for (int i = 0; i < consumptionCycles; i++)
            {
                warehouse.CurrentFoodTons =
                    Math.Max(0, warehouse.CurrentFoodTons - warehouse.ConsumptionPerCycleTons);
            }
        }

        foreach (HydroponizedFarm farm in farms)
        {
            if (!farm.Operational)
                continue;

            if (warehouse.CurrentFoodTons >= warehouse.MaximumCapacityTons)
                continue;

            double productionElapsed = (now - farm.LastProductionTime).TotalSeconds;

            if (productionElapsed < 30.0)
                continue;

            int productionCycles = (int)(productionElapsed / 30.0);
            farm.LastProductionTime = farm.LastProductionTime.AddSeconds(productionCycles * 30.0);

            for (int i = 0; i < productionCycles; i++)
            {
                if (warehouse.CurrentFoodTons >= warehouse.MaximumCapacityTons)
                    break;

                warehouse.CurrentFoodTons =
                    Math.Min(warehouse.MaximumCapacityTons,
                             warehouse.CurrentFoodTons + warehouse.ProductionPerCycleTons);
            }
        }
    }

    // =========================================================
    // CONSTRUCTION UNIT AVAILABILITY
    // =========================================================

    static bool CanUseConstructionUnits(
        FieldUnit ar3, FieldUnit ar4,
        FieldUnit ar5, FieldUnit ar6, FieldUnit ar7, FieldUnit ar8, FieldUnit ar9,
        FieldUnit rover2, FieldUnit rover3, FieldUnit rover4,
        out int availableAndroids, out int availableRovers)
    {
        availableAndroids = 0;
        availableRovers = 0;

        FieldUnit[] androids = { ar3, ar4, ar5, ar6, ar7, ar8, ar9 };
        FieldUnit[] rovers = { rover2, rover3, rover4 };

        foreach (FieldUnit android in androids)
        {
            if (android != null && android.Available && android.Status == "BASE")
                availableAndroids++;
        }

        foreach (FieldUnit rover in rovers)
        {
            if (rover != null && rover.Available && rover.Status == "BASE")
                availableRovers++;
        }

        return availableAndroids > 0 && availableRovers > 0;
    }

    // =========================================================
    // ASSIGN CONSTRUCTION UNITS
    // =========================================================

    static void AssignConstructionUnits(
        int androidsRequired, int roversRequired, int personnelRequired,
        int availableCrewBefore,
        FieldUnit ar3, FieldUnit ar4,
        FieldUnit ar5, FieldUnit ar6, FieldUnit ar7, FieldUnit ar8, FieldUnit ar9,
        FieldUnit rover2, FieldUnit rover3, FieldUnit rover4,
        ref int availableCrew, ref int assignedCrew)
    {
        FieldUnit[] androids = { ar3, ar4, ar5, ar6, ar7, ar8, ar9 };
        int assignedAndroids = 0;

        foreach (FieldUnit android in androids)
        {
            if (android != null && android.Available && android.Status == "BASE" &&
                assignedAndroids < androidsRequired)
            {
                android.Available = false;
                android.Status = "CONSTRUCTING";
                assignedAndroids++;
            }
        }

        FieldUnit[] rovers = { rover2, rover3, rover4 };
        int assignedRovers = 0;

        foreach (FieldUnit rover in rovers)
        {
            if (rover != null && rover.Available && rover.Status == "BASE" &&
                assignedRovers < roversRequired)
            {
                rover.Available = false;
                rover.Status = "CONSTRUCTING";
                assignedRovers++;
            }
        }

        availableCrew -= personnelRequired;
        assignedCrew += personnelRequired;
    }

    // =========================================================
    // RELEASE CONSTRUCTION UNITS
    // =========================================================

    static void ReleaseConstructionUnits(
        int androidsRequired, int roversRequired, int personnelRequired,
        FieldUnit ar3, FieldUnit ar4,
        FieldUnit ar5, FieldUnit ar6, FieldUnit ar7, FieldUnit ar8, FieldUnit ar9,
        FieldUnit rover2, FieldUnit rover3, FieldUnit rover4,
        ref int availableCrew, ref int assignedCrew)
    {
        FieldUnit[] androids = { ar3, ar4, ar5, ar6, ar7, ar8, ar9 };
        int releasedAndroids = 0;

        foreach (FieldUnit android in androids)
        {
            if (android != null && android.Status == "CONSTRUCTING" &&
                releasedAndroids < androidsRequired)
            {
                android.Status = "BASE";
                android.Available = true;
                releasedAndroids++;
            }
        }

        FieldUnit[] rovers = { rover2, rover3, rover4 };
        int releasedRovers = 0;

        foreach (FieldUnit rover in rovers)
        {
            if (rover != null && rover.Status == "CONSTRUCTING" &&
                releasedRovers < roversRequired)
            {
                rover.Status = "BASE";
                rover.Available = true;
                releasedRovers++;
            }
        }

        availableCrew += personnelRequired;
        assignedCrew -= personnelRequired;
    }

    // =========================================================
    // FIND HIDDEN BASALT
    // =========================================================

    static void FindHiddenBasalt(
        char[,] grid,
        int labX,
        int labY,
        ref int basaltX,
        ref int basaltY,
        Random rand)
    {
        List<Point> candidates = new List<Point>();

        for (int y = labY - 5; y <= labY + 5; y++)
        {
            for (int x = labX - 5; x <= labX + 5; x++)
            {
                int distance = Math.Abs(x - labX) + Math.Abs(y - labY);

                if (IsInside(grid, x, y) &&
                    distance >= 2 &&
                    distance <= 4 &&
                    (grid[y, x] == '.' || grid[y, x] == 'R'))
                {
                    candidates.Add(new Point(x, y));
                }
            }
        }

        if (candidates.Count == 0)
        {
            basaltX = labX + 2;
            basaltY = labY;
            return;
        }

        Point selected = candidates[rand.Next(candidates.Count)];
        basaltX = selected.X;
        basaltY = selected.Y;
    }

    // =========================================================
    // A* PATHFINDING
    // =========================================================

    static List<Point> FindPath(
        char[,] grid,
        int startX,
        int startY,
        int targetX,
        int targetY)
    {
        List<Point> empty = new List<Point>();

        if (!IsInside(grid, startX, startY) || !IsInside(grid, targetX, targetY))
            return empty;

        List<Node> open = new List<Node>();
        HashSet<string> closed = new HashSet<string>();

        Node start = new Node(startX, startY, null, 0,
            Manhattan(startX, startY, targetX, targetY));

        open.Add(start);

        int[,] directions = { { -1, 0 }, { 1, 0 }, { 0, -1 }, { 0, 1 } };

        while (open.Count > 0)
        {
            Node current = open[0];

            foreach (Node node in open)
            {
                if (node.F < current.F ||
                    (node.F == current.F && node.H < current.H))
                {
                    current = node;
                }
            }

            open.Remove(current);
            closed.Add(Key(current.X, current.Y));

            if (current.X == targetX && current.Y == targetY)
                return ReconstructPath(current);

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int nextX = current.X + directions[i, 0];
                int nextY = current.Y + directions[i, 1];

                if (!IsInside(grid, nextX, nextY))
                    continue;

                if (closed.Contains(Key(nextX, nextY)))
                    continue;

                if (!CanUnitEnter(grid[nextY, nextX], nextX, nextY, targetX, targetY))
                    continue;

                int newG = current.G + 1;
                Node existing = open.Find(n => n.X == nextX && n.Y == nextY);

                if (existing == null)
                {
                    int h = Manhattan(nextX, nextY, targetX, targetY);
                    open.Add(new Node(nextX, nextY, current, newG, h));
                }
                else if (newG < existing.G)
                {
                    existing.G = newG;
                    existing.Parent = current;
                }
            }
        }

        return empty;
    }

    // =========================================================
    // CAN UNIT ENTER CELL
    // =========================================================

    static bool CanUnitEnter(
        char cell,
        int x,
        int y,
        int targetX,
        int targetY)
    {
        if (x == targetX && y == targetY)
            return true;

        if (cell == 'M' || cell == 'L' || cell == 'W' || cell == 'S' ||
            cell == 'H' || cell == 'F' || cell == 'D' ||
            cell == 'P' || cell == 'O' || cell == 'R' || cell == 'C')
            return false;

        if (cell == 'B' || cell == 'R' || cell == 'P' || cell == '.')
            return true;

        if (cell == 'A' || cell == 'r')
            return false;

        return false;
    }

    // =========================================================
    // RECONSTRUCT A* PATH
    // =========================================================

    static List<Point> ReconstructPath(Node node)
    {
        List<Point> path = new List<Point>();
        Node current = node;

        while (current.Parent != null)
        {
            path.Add(new Point(current.X, current.Y));
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }

    // =========================================================
    // MANHATTAN DISTANCE
    // =========================================================

    static int Manhattan(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }

    // =========================================================
    // NODE KEY
    // =========================================================

    static string Key(int x, int y)
    {
        return x + "," + y;
    }

    // =========================================================
    // ASK NUMBER
    // =========================================================

    static int AskNumber(string message, int min, int max)
    {
        int value;

        while (true)
        {
            Console.Write($"{message} ({min}-{max}): ");

            if (int.TryParse(Console.ReadLine(), out value))
            {
                if (value >= min && value <= max)
                    return value;
            }

            Console.WriteLine($"Please enter a number between {min} and {max}.");
        }
    }

    // =========================================================
    // ASK COORDINATE
    // =========================================================

    static int AskCoordinate(string axis)
    {
        while (true)
        {
            Console.Write($"{axis}: ");

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                if (value >= 0 && value <= 29)
                    return value;
            }

            Console.WriteLine("Please enter a number between 0 and 29.");
        }
    }

    // =========================================================
    // CHECK GRID BOUNDARIES
    // =========================================================

    static bool IsInside(char[,] grid, int x, int y)
    {
        return y >= 0 && y < grid.GetLength(0) &&
               x >= 0 && x < grid.GetLength(1);
    }

    // =========================================================
    // UPDATE WIND TURBINES
    // =========================================================

    static void UpdateWindTurbines(List<WindTurbine> windTurbines, Random rand)
    {
        foreach (WindTurbine turbine in windTurbines)
        {
            int windChange = rand.Next(-10, 11);
            turbine.WindDynamics += windChange;
            turbine.WindDynamics = Math.Max(0, Math.Min(100, turbine.WindDynamics));

            turbine.CurrentPower =
                turbine.MaxPowerPerHour *
                (turbine.WindDynamics / 100.0) *
                (turbine.Condition / 100.0);
        }
    }

    // =========================================================
    // DRAW GRID
    // =========================================================

    static void DrawGrid(char[,] grid, List<FieldUnit> units)
    {
        Console.WriteLine("              APHRODITE'S CHILDREN");
        Console.WriteLine("                    MISSION 01 & 02");
        Console.WriteLine();

        Console.Write("     ");

        for (int column = 0; column < grid.GetLength(1); column++)
            Console.Write($"{column,2} ");

        Console.WriteLine();

        Console.Write("     ");

        for (int column = 0; column < grid.GetLength(1); column++)
            Console.Write("---");

        Console.WriteLine();

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            Console.Write($"{row,2} | ");

            for (int column = 0; column < grid.GetLength(1); column++)
            {
                char displayChar = grid[row, column];

                if (units != null)
                {
                    foreach (FieldUnit unit in units)
                    {
                        if (unit.X == column && unit.Y == row)
                        {
                            displayChar = unit.Symbol;
                            break;
                        }
                    }
                }

                Console.Write($" {displayChar} ");
            }

            Console.WriteLine();
        }
    }

    // =========================================================
    // INTRO SCREEN
    // =========================================================

    static void ShowIntro()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine();
        Console.WriteLine("          A P H R O D I T E ' S");
        Console.WriteLine("               C H I L D R E N");
        Console.WriteLine();
        Console.WriteLine("                    v.1.5");
        Console.WriteLine();
        Console.WriteLine("          C O L O N I Z A T I O N");
        Console.WriteLine("                 P R O J E C T");
        Console.WriteLine();
        Console.WriteLine("==================================================");
        Console.WriteLine();
    }

    // =========================================================
    // WAIT FOR START
    // =========================================================

    static void WaitForStart()
    {
        string message = "              PRESS ENTER TO START";

        while (true)
        {
            try
            {
                Console.SetCursorPosition(0, 13);
                Console.Write(message);
                Thread.Sleep(600);
                Console.SetCursorPosition(0, 13);
                Console.Write(new string(' ', message.Length));
                Thread.Sleep(600);
            }
            catch
            {
                // If the window is small, just continue.
            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                    break;
            }
        }

        Console.CursorVisible = true;
    }
}