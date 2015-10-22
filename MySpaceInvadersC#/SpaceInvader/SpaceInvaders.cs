using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace SpaceInvader
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceInvaders : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Image Variables
        Texture2D StarfieldImg;
        Texture2D Homescreenimg;
        Texture2D Endscreenimg;
        Texture2D GameOverimg;
        Texture2D HelpBackgroundimg;
        Texture2D InvaderImg;
        Texture2D InvaderImg2;
        Texture2D InvaderImg3;
        Texture2D InvaderImg4;
        Texture2D BossInvaderImg;
        Texture2D AltInvaderImg;
        Texture2D AltInvaderImg2;
        Texture2D AltInvaderImg3;
        Texture2D AltInvaderImg4;
        Texture2D RocketLauncherImg;
        Texture2D MissileImg;
        Texture2D MissileImg2;
        Texture2D MissileImg3;
        Texture2D AlienMissileImg;
        Texture2D AlienMissileImg2;
        Texture2D BossMissileImg;

        //Rocket Variables
        int RocketXPos;

        //Alien & Boss Variables
        int AlienDirection;
        int AlienSpeed;
        int BossAlienDirection;
        int BossAlienSpeed;

        //Alien Array Variables
        Invader[] Invaders;
        Invader[] InvadersOne;
        Invader[] InvadersTwo;
        Invader[] InvadersThree;
        Invader[] InvadersFour;

        //Boss Class Variables
        BossAlien BossAlien;
        int BossLife;

        //Timer implemented for randomised shooting
        double Ticks;
        double Timer;
        double Timer2;
        double Timer3;
        double Timer4;
        double BossTimer;

        //Check whether missles have been fired (Alien and Player)
        Missile MissileFired;
        AlienMissile AlienMissileFired;
        AlienMissile AlienMissileFired2;
        AlienMissile AlienMissileFired3;
        AlienMissile AlienMissileFired4;
        AlienMissile BossMissileFired;

        //Player Variables
        int PlayerScore;
        int AmountofLives;
        int WeaponsCount;

        //Font variables
        SpriteFont GameFont;

        //Sound variables
        SoundEffect ExplosionSound;
        SoundEffectInstance ExplosionSoundInstance;
        SoundEffect ShootSound;
        SoundEffectInstance ShootSoundInstance;
        SoundEffect Shoot2Sound;
        SoundEffectInstance Shoot2SoundInstance;
        SoundEffect Shoot3Sound;
        SoundEffectInstance Shoot3SoundInstance;
        SoundEffect AlienShootSound;
        SoundEffectInstance AlienShootSoundInstance;
        SoundEffect BossShootSound;
        SoundEffectInstance BossShootSoundInstance;
        SoundEffect InvaderkilledSound;
        SoundEffectInstance InvaderkilledSoundInstance;
        SoundEffect BosskilledSound;
        SoundEffectInstance BosskilledSoundInstance;
        SoundEffect PlayerKilledSound;
        SoundEffectInstance PlayerKilledSoundInstance;
        SoundEffect Weapon1Sound;
        SoundEffectInstance Weapon1SoundInstance;
        SoundEffect Weapon2Sound;
        SoundEffectInstance Weapon2SoundInstance;
        SoundEffect JetSound;
        SoundEffectInstance JetSoundInstance;
        SoundEffect StarwarsSound;
        SoundEffectInstance StarwarsSoundInstance;
        SoundEffect VictorySound;
        SoundEffectInstance VictorySoundInstance;
        SoundEffect GameOverSound;
        SoundEffectInstance GameOverSoundInstance;
        SoundEffect IntroSound;
        SoundEffectInstance IntroSoundInstance;

        //Gamestate variables
        int GameState;

        //CS File
        public SpaceInvaders()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024; //Changing these values modifies the game screen size
            graphics.PreferredBackBufferHeight = 768;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content"; //Resources are retrieved from 'Content' folder
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
     
        //Initialize Variables/Gamestates
        protected override void Initialize()
        {
            InitialiseGameVariables();
            GameState = 1;
            base.Initialize();
        }

        //Initialise Ingame Variables
        public void InitialiseGameVariables()
        {
            RocketXPos = 650;         //Set Rocket X value
            AlienDirection = -1;      //Set default direction
            AlienSpeed = 25;          //Sets a default speed (These can be changed in-game to increase difficulty)
            
            BossAlienDirection = -1; 
            BossAlienSpeed = 100;

            //Declaring the Alien arrays:
            Invaders = new Invader[11];     //11 columns across
            InvadersOne = new Invader[11];
            InvadersTwo = new Invader[11];
            InvadersThree = new Invader[11];
            InvadersFour = new Invader[11];

            int XPos = 512;                 // Sets the default X coordinate for Aliens
            for (int Count = 0; Count < 11; Count++)    //Checks the count for amount of aliens
            {
                //First Array
                Invaders[Count] = new Invader();
                Invaders[Count].SetXPos(XPos);
                Invaders[Count].SetYPos(100);   //Each line of alien has a different Y coordinate
                //Second Array
                InvadersOne[Count] = new Invader();
                InvadersOne[Count].SetXPos(XPos);
                InvadersOne[Count].SetYPos(135);
                //Third Array
                InvadersTwo[Count] = new Invader();
                InvadersTwo[Count].SetXPos(XPos);
                InvadersTwo[Count].SetYPos(155);
                //Fourth Array
                InvadersThree[Count] = new Invader();
                InvadersThree[Count].SetXPos(XPos);
                InvadersThree[Count].SetYPos(180);
                //Fifth Array
                InvadersFour[Count] = new Invader();
                InvadersFour[Count].SetXPos(XPos);
                InvadersFour[Count].SetYPos(205);

                XPos = XPos + 32;
            }

            int BossXPos = 512;     //Sets defualt X position for Boss Alien
            BossAlien= new BossAlien();
            BossAlien.SetXPos(BossXPos);
            BossAlien.SetYPos(0);

            Ticks = 0; //Shows the amount of time passed
            Timer = 0; //Timer for each variable e.g. Alien & Boss
            Timer2 = 0;
            Timer3 = 0;
            Timer4 = 0;
            MissileFired = null;    //Missile values are set to null at start
            AlienMissileFired = null;
            AlienMissileFired2 = null;
            BossMissileFired = null;
            PlayerScore = 0;    //Player score set to 0 at start
            AmountofLives = 3; //Amount of lives the player at start of game
            BossLife = 3;     //Amount of lives Boss has at start of game
            WeaponsCount = 1; //Checks what weapon the player is using & allows switching between them
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            //Load Images
            StarfieldImg = Content.Load<Texture2D>("Starfield");
            Homescreenimg = Content.Load<Texture2D>("Homescreen");
            Endscreenimg = Content.Load<Texture2D>("Endscreen");
            GameOverimg = Content.Load<Texture2D>("GameOver");
            HelpBackgroundimg = Content.Load<Texture2D>("Helpbackground");
            InvaderImg = Content.Load<Texture2D>("inv1");
            AltInvaderImg = Content.Load<Texture2D>("inv12");
            BossInvaderImg = Content.Load<Texture2D>("AlienBoss");
            InvaderImg2 = Content.Load<Texture2D>("inv2");
            AltInvaderImg2 = Content.Load<Texture2D>("inv22");
            InvaderImg3 = Content.Load<Texture2D>("inv3");
            AltInvaderImg3 = Content.Load<Texture2D>("inv32");
            InvaderImg4 = Content.Load<Texture2D>("inv4");
            AltInvaderImg4 = Content.Load<Texture2D>("inv42");
            RocketLauncherImg = Content.Load<Texture2D>("Aircraft");
            MissileImg = Content.Load<Texture2D>("bullet");
            MissileImg2 = Content.Load<Texture2D>("bullet2");
            MissileImg3 = Content.Load<Texture2D>("bullet3");
            AlienMissileImg = Content.Load<Texture2D>("AlienMissileImg");
            AlienMissileImg2 = Content.Load<Texture2D>("AlienMissileImg2");
            BossMissileImg = Content.Load<Texture2D>("BossMissileImg");

            //Load Fonts
            GameFont = Content.Load<SpriteFont>("GameFont");

            //Load Sounds
            ExplosionSound = Content.Load<SoundEffect>("explosion");
            ExplosionSoundInstance = ExplosionSound.CreateInstance();
            ShootSound = Content.Load<SoundEffect>("shoot");
            ShootSoundInstance = ShootSound.CreateInstance();
            Shoot2Sound = Content.Load<SoundEffect>("shoot2");
            Shoot2SoundInstance = Shoot2Sound.CreateInstance();
            Shoot3Sound = Content.Load<SoundEffect>("shoot3");
            Shoot3SoundInstance = Shoot3Sound.CreateInstance();
            AlienShootSound = Content.Load<SoundEffect>("AlienMissile");
            AlienShootSoundInstance = AlienShootSound.CreateInstance();
            BossShootSound = Content.Load<SoundEffect>("BossMissile");
            BossShootSoundInstance = BossShootSound.CreateInstance();
            InvaderkilledSound = Content.Load<SoundEffect>("Invaderkilled");
            InvaderkilledSoundInstance = InvaderkilledSound.CreateInstance();
            BosskilledSound = Content.Load<SoundEffect>("Bosskilled");
            BosskilledSoundInstance = BosskilledSound.CreateInstance();
            PlayerKilledSound = Content.Load<SoundEffect>("PlayerKilled");
            PlayerKilledSoundInstance = PlayerKilledSound.CreateInstance();
            Weapon1Sound = Content.Load<SoundEffect>("Weapon switch1");
            Weapon1SoundInstance = Weapon1Sound.CreateInstance();
            Weapon2Sound = Content.Load<SoundEffect>("Weapon switch2");
            Weapon2SoundInstance = Weapon2Sound.CreateInstance();
            JetSound = Content.Load<SoundEffect>("JetSound");
            JetSoundInstance = JetSound.CreateInstance();
            StarwarsSound = Content.Load<SoundEffect>("star wars");
            StarwarsSoundInstance = StarwarsSound.CreateInstance();
            VictorySound = Content.Load<SoundEffect>("Victory");
            VictorySoundInstance = VictorySound.CreateInstance();
            GameOverSound = Content.Load<SoundEffect>("GameOversong");
            GameOverSoundInstance = GameOverSound.CreateInstance();
            IntroSound = Content.Load<SoundEffect>("Introsong");
            IntroSoundInstance = IntroSound.CreateInstance();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
            public void UpdateStarted(GameTime currentTime)     //Update Method for Home screen
        {
            IntroSoundInstance.Play();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) //Pressing Enter brings the player to the help screen i.e. Gamestate 5
            {
                GameState = 5;
                return;
            }
        }

        public void UpdatePlaying(GameTime currentTime) //Update method for Gameplay screen
        // TODO: Add your update logic here 
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                GameState = 3;
                return;
            }
            //Implemented to make testing easier
            //easily switch through different game states
            if (Keyboard.GetState().IsKeyDown(Keys.F1))
            {
                StarwarsSoundInstance.Stop();
                GameState = 1;
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F2))
            {
                GameState = 2;
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F3))
            {
                GameState = 3;
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F4))
            {
                GameState = 4;
                return;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.F5))
            {
                GameState = 5;
                return;
            }
            //Allows switching between weapons (Also allows easier testing cos of faster gameplay)
            if (Keyboard.GetState().IsKeyDown(Keys.D1))
            {
                WeaponsCount = 1;
                Weapon1SoundInstance.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D2))
            {
                WeaponsCount = 2;
                Weapon2SoundInstance.Play();
            }

            // These statements check to see if there are any invaders remaining to shoot
             bool IsBossInvaderRemaining = false;
                if (BossAlien != null)
                {
                    IsBossInvaderRemaining = true;      //If statement checks if the Boss alien is still remaining
                }

            bool IsInvaderRemaining = false;
            for (int Count = 0; Count < 11; Count++)
            {
                //First Array
                if (Invaders[Count] != null)        //If statement is null that means there are no Aliens left for that particular Array
                {
                    IsInvaderRemaining = true;
                    break;
                }
                //Second Array
                if (InvadersOne[Count] != null)
                {
                    IsInvaderRemaining = true;
                    break;
                }
                //Third Array
                if (InvadersTwo[Count] != null)
                {
                    IsInvaderRemaining = true;
                    break;
                }
                //Fourth Array
                if (InvadersThree[Count] != null)
                {
                    IsInvaderRemaining = true;
                    break;
                }
                //Fifth Array
                if (InvadersFour[Count] != null)
                {
                    IsInvaderRemaining = true;
                    break;
                }
            }

            // If there are no invaders then move to end game state (Gamestate 3)
            if (!IsInvaderRemaining & !IsBossInvaderRemaining)
            {    
                GameState = 3;
                return;
            }

            //Player Missile
            if (BossAlien != null & (WeaponsCount == 1 || WeaponsCount ==2))    //Checks if Boss Alien is destroyed and what weapon count it is on
            {                                                                   //This allows weapons power up if Boss is destroyed and switching of weapons
                if (MissileFired != null)
                {
                    MissileFired.Move();
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Space))                  //Fires missile from the location of rocket, if Space bar is pressed
                {
                    MissileFired = new Missile(RocketXPos, 650);
                    ShootSoundInstance.Play();                                  //Sound is also played
                }
            }
            //Switch weapons //Used for testing to save time
            if (WeaponsCount == 2)                                      
            {
                if (MissileFired != null)
                {
                    MissileFired.Move2();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    MissileFired = new Missile(RocketXPos, 650);
                    Shoot3SoundInstance.Play();
                }
            }
            //If Boss alien has been destroyed use second missile (Upgrade weapons/ Power up)
            if (BossAlien == null)
            {
                if (MissileFired != null)
                {
                    MissileFired.Move3();
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    MissileFired = new Missile(RocketXPos, 650);
                    if (WeaponsCount == 1)
                    {
                        Shoot2SoundInstance.Play();
                    }
                }
            }
            //Alien Missile
            if (AlienMissileFired != null)  //If Alien missile is not equal to Null then alien missile starts moving
            {
                AlienMissileFired.Move();
            }
            if (AlienMissileFired2 != null)
            {
                AlienMissileFired2.Move();
            }
            if (AlienMissileFired3 != null)
            {
                AlienMissileFired3.Move();
            }
            if (AlienMissileFired4 != null)
            {
                AlienMissileFired4.Move();
            }
            if (BossMissileFired != null)
            {
                BossMissileFired.Move();
            }
            Timer = Timer + currentTime.ElapsedGameTime.Milliseconds;   //Timer to control the automation of alien/Boss shooting
            Timer2 = Timer2 + currentTime.ElapsedGameTime.Milliseconds;
            Timer3 = Timer3 + currentTime.ElapsedGameTime.Milliseconds;
            Timer4 = Timer4 + currentTime.ElapsedGameTime.Milliseconds; 
            BossTimer = BossTimer + currentTime.ElapsedGameTime.Milliseconds;

            //First Array
            if (Timer > 2865)   //The timer values can be adjusted at each array to change the frequency of the automatic shooting
            {
                for (int Count = 0; Count < 11; Count++)                                                               //Checks the amount of aliens 
                {
                    if (Invaders[Count] != null)                                                                        //If available then fires from alien at the player
                    {
                        AlienMissileFired = new AlienMissile(Invaders[Count].GetXPos(), Invaders[Count].GetYPos());     //Fires from the X and Y coordinates of existing aliens
                        AlienShootSoundInstance.Play();
                        Timer = 0;                                                                                      //Timer is reset back to 0 for continious shooting
                    }
                    }
              }
            //Second Array
            if (Timer2 > 4234)
            {
                for (int Count = 0; Count < 11; Count++)
                {
                    if (InvadersTwo[Count] != null)
                    {
                        AlienMissileFired2 = new AlienMissile(InvadersTwo[Count].GetXPos(), InvadersTwo[Count].GetYPos());
                        AlienShootSoundInstance.Play();
                        Timer2 = 0;
                    }
                }
            }
            //Third Array
            if (Timer3 > 6403)
            {
                for (int Count = 0; Count < 11; Count++)
                {
                    if (InvadersThree[Count] != null)
                    {
                        AlienMissileFired3 = new AlienMissile(InvadersThree[Count].GetXPos(), InvadersThree[Count].GetYPos());
                        AlienShootSoundInstance.Play();
                        Timer3 = 0;
                    }
                }
            }
            //Fourth Array
            if (Timer4 > 8000)
            {
                for (int Count = 0; Count < 11; Count++)
                {
                    if (InvadersFour[Count] != null)
                    {
                        AlienMissileFired4 = new AlienMissile(InvadersFour[Count].GetXPos(), InvadersFour[Count].GetYPos());
                        AlienShootSoundInstance.Play();
                        Timer4 = 0;
                    }
                }
            }
            //Boss Missile
            if (BossTimer > 7800)
            {    
                    if (BossAlien != null)
                    {
                        BossMissileFired = new AlienMissile(BossAlien.GetXPos(), BossAlien.GetYPos());
                        BossShootSoundInstance.Play();
                        BossTimer = 0;
                    }
            }
             //Controls movement of the Aircraft and sound effects
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                if (BossAlien != null)
                {
                RocketXPos = RocketXPos - 4;      //Changing this will affect the speed of the rocket
                JetSoundInstance.Play();
                 }
                if (BossLife == 2)
                {
                    RocketXPos = RocketXPos - 5;     //Upgrade if boss is hit (Move faster)
                    JetSoundInstance.Play();
                }
                if (BossLife == 1)
                {
                    RocketXPos = RocketXPos - 6;      //Upgrade if boss is hit (Move faster)
                    JetSoundInstance.Play();
                }
                if (BossAlien == null & BossLife == 0)
                {
                    RocketXPos = RocketXPos - 8;      //Upgrade if boss is destroyed (Move faster)
                    JetSoundInstance.Play();
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                if (BossAlien != null)
                {
                    RocketXPos = RocketXPos + 4;      //Changing this will affect the speed of the rocket
                    JetSoundInstance.Play();
                }
                if (BossLife == 2)
                {
                    RocketXPos = RocketXPos + 5;      //Upgrade if boss is hit (Move faster)
                    JetSoundInstance.Play();
                }
                if (BossLife == 1)
                {
                    RocketXPos = RocketXPos + 6;      //Upgrade if boss is hit (Move faster)
                    JetSoundInstance.Play();
                }
                if (BossAlien == null & BossLife == 0)
                {
                    RocketXPos = RocketXPos + 8;      //Upgrade if boss is destroyed (Move faster)
                    JetSoundInstance.Play();
                }
            }

            if (RocketXPos < 100)       //Limits the Rockets movement to prevent it from going off the screen
            {
                RocketXPos = 100;
            }

            if (RocketXPos > 924)
            {
                RocketXPos = 924;
            }

            //Controls movement of Alien
            Ticks = Ticks + currentTime.ElapsedGameTime.TotalMilliseconds;  //Calculates the ingame time for the movement of aliens

            if (Ticks > 500)    //if the timer is more than 500 then moves the alien horizontal
            {
                for (int Count = 0; Count < 11; Count++)
                {
                    //First Array
                    if (Invaders[Count] != null)
                    {
                        Invaders[Count].MoveHorizontal(AlienSpeed * AlienDirection);
                    }
                    //second array
                    if (InvadersOne[Count] != null)
                    {
                        InvadersOne[Count].MoveHorizontal(AlienSpeed * AlienDirection);
                    }
                    //Third array
                    if (InvadersTwo[Count] != null)
                    {
                        InvadersTwo[Count].MoveHorizontal(AlienSpeed * AlienDirection);
                    }
                    //Fourth array
                    if (InvadersThree[Count] != null)
                    {
                        InvadersThree[Count].MoveHorizontal(AlienSpeed * AlienDirection);
                    }
                    //Fifth array
                    if (InvadersFour[Count] != null)
                    {
                        InvadersFour[Count].MoveHorizontal(AlienSpeed * AlienDirection);
                    }
                }
                //Boss Alien movement
                if (Ticks > 500)
                {
                    if (BossAlien != null)
                    {
                        BossAlien.MoveHorizontal(BossAlienSpeed * BossAlienDirection);
                    }
                    if (BossLife == 1)
                    {
                        BossAlienSpeed = 105;
                        BossAlien.MoveHorizontal(BossAlienSpeed * BossAlienDirection);
                    }
                }
                Invader LeftMostInvader = null; //Aliens set to null
                Invader RightMostInvader = null;

                for (int Count = 0; Count < 11; Count++)
                {
                    //First Array
                    if (Invaders[Count] != null)
                    {
                        LeftMostInvader = Invaders[Count];
                        break;
                    }
                    //second array
                    if (InvadersOne[Count] != null)
                    {
                        LeftMostInvader = InvadersOne[Count];
                        break;
                    }
                    //Third array
                    if (InvadersTwo[Count] != null)
                    {
                        LeftMostInvader = InvadersTwo[Count];
                        break;
                    }
                    //Fourth array
                    if (InvadersThree[Count] != null)
                    {
                        LeftMostInvader = InvadersThree[Count];
                        break;
                    }
                    //Fifth array
                    if (InvadersFour[Count] != null)
                    {
                        LeftMostInvader = InvadersFour[Count];
                        break;
                    }
                }

                for (int Count = 10; Count > 0; Count--)
                {
                    //First Array
                    if (Invaders[Count] != null)
                    {
                        RightMostInvader = Invaders[Count];
                        break;
                    }
                    //second array
                    if (InvadersOne[Count] != null)
                    {
                        RightMostInvader = InvadersOne[Count];
                        break;
                    }
                    //Third array
                    if (InvadersTwo[Count] != null)
                    {
                        RightMostInvader = InvadersTwo[Count];
                        break;
                    }
                    //Fourth array
                    if (InvadersThree[Count] != null)
                    {
                        RightMostInvader = InvadersThree[Count];
                        break;
                    }
                    //Fifth array
                    if (InvadersFour[Count] != null)
                    {
                        RightMostInvader = InvadersFour[Count];
                        break;
                    }
                }

                //Controls alien speed/difficulty
                if (LeftMostInvader != null)                     //Checks if aliens is available
                {
                    if (LeftMostInvader.GetYPos() > 200)         //Increases alien speed after to goes pass a certain Y coordinate
                    {
                        AlienSpeed = 50;
                    }
                }
                if (RightMostInvader != null)
                {
                    if (RightMostInvader.GetYPos() > 200)
                    {
                        AlienSpeed = 50;
                    }
                }
                  if (LeftMostInvader != null)
                {
                if (LeftMostInvader.GetYPos() > 300)
                {
                    AlienSpeed = 60;
                }
                }
                  if (RightMostInvader != null)
                  {
                      if (RightMostInvader.GetYPos() > 300)
                      {
                          AlienSpeed = 60;
                      }
                  }

                //Game Over when aliens reach the bottom, navigates to Gameover screen
                 if (LeftMostInvader != null)
                  {
                if (LeftMostInvader.GetYPos() > 550)
                {
                    GameState = 4;          //Goes to gameover screen
                    return;
                }
                }
                      if (RightMostInvader != null)
                  {
                if (RightMostInvader.GetYPos() > 550)
                {
                    GameState = 4;
                    return;
                }
                  }

                //Checks Boss alien direction
                if (BossAlien != null)
                    {
                     if (BossAlien.GetXPos() < 96)   //Limits the aliens direction to prevent it from going off the screen
                     {
                    BossAlienDirection = +1;
                    int BossXPos = 96;
                    if (BossAlien != null)
                    {
                        BossAlien.SetXPos(BossXPos);
                    }
                    BossXPos = BossXPos + BossInvaderImg.Width;
                }
                    }
                  if (BossAlien != null)
                    {
                if (BossAlien.GetXPos() > 924)
                {
                    BossAlienDirection = -1;
                    int BossXPos = 924 - BossInvaderImg.Width * 10;
                    if (BossAlien != null)
                    {
                        BossAlien.SetXPos(BossXPos);
                    }
                    BossXPos = BossXPos + BossInvaderImg.Width;
                       }
                    }
                //Checks  alien direction
                if (LeftMostInvader != null)
                  {
                if (LeftMostInvader.GetXPos() < 96)
                {
                    AlienDirection = +1;
                    int XPos = 96;
                    for (int Count = 0; Count < 11; Count++)    //Checks the array for the position of the aliens before moving them vertically once they reached the side
                    {
                        if (Invaders[Count] != null)
                        {
                            Invaders[Count].MoveVertical(20);
                            Invaders[Count].SetXPos(XPos);
                        }
                        //Second Array
                        if (InvadersOne[Count] != null)
                        {
                            InvadersOne[Count].MoveVertical(20);
                            InvadersOne[Count].SetXPos(XPos);
                        }
                        //Third Array
                        if (InvadersTwo[Count] != null)
                        {
                            InvadersTwo[Count].MoveVertical(20);
                            InvadersTwo[Count].SetXPos(XPos);
                        }
                        //Fourth Array
                        if (InvadersThree[Count] != null)
                        {
                            InvadersThree[Count].MoveVertical(20);
                            InvadersThree[Count].SetXPos(XPos);
                        }
                        //Fifth Array
                        if (InvadersFour[Count] != null)
                        {
                            InvadersFour[Count].MoveVertical(20);
                            InvadersFour[Count].SetXPos(XPos);
                        }
                        XPos = XPos + InvaderImg.Width;
                    }
                }
                  }
                if (RightMostInvader != null)       //Does the same check with rightmost alien
                {
                    if (RightMostInvader.GetXPos() > 924)
                    {
                        AlienDirection = -1;
                        int XPos = 924 - InvaderImg.Width * 10;
                        for (int Count = 0; Count < 11; Count++)
                        {
                            if (Invaders[Count] != null)
                            {
                                Invaders[Count].MoveVertical(20);
                                Invaders[Count].SetXPos(XPos);
                            }
                            //Second Array
                            if (InvadersOne[Count] != null)
                            {
                                InvadersOne[Count].MoveVertical(20);
                                InvadersOne[Count].SetXPos(XPos);
                            }
                            //Third Array
                            if (InvadersTwo[Count] != null)
                            {
                                InvadersTwo[Count].MoveVertical(20);
                                InvadersTwo[Count].SetXPos(XPos);
                            }
                            //Fourth Array
                            if (InvadersThree[Count] != null)
                            {
                                InvadersThree[Count].MoveVertical(20);
                                InvadersThree[Count].SetXPos(XPos);
                            }
                            //Fifth Array
                            if (InvadersFour[Count] != null)
                            {
                                InvadersFour[Count].MoveVertical(20);
                                InvadersFour[Count].SetXPos(XPos);
                            }
                            XPos = XPos + InvaderImg.Width;
                        }
                    }
                }   

                Ticks = 0;
            }

            //Checks whether alien missile intersects with Player 
            //If intesects then Lose life or Game Over
            //First array
                       if (AlienMissileFired != null)
                   {
                       Rectangle rectAlienMissile = new Rectangle((int)AlienMissileFired.GetPosition().X, (int)AlienMissileFired.GetPosition().Y, AlienMissileImg.Width, AlienMissileImg.Height);
                Rectangle rectRocket = new Rectangle(RocketXPos, 700, RocketLauncherImg.Width, RocketLauncherImg.Height);

                        if (rectAlienMissile.Intersects(rectRocket))
                        {
                            if (AmountofLives == 0)
                            {
                            AlienMissileFired = null;
                            GameState = 4;
                            PlayerKilledSoundInstance.Play();
                            }
                            AmountofLives = AmountofLives - 1;      //Player life is subtract from total life
                            AlienMissileFired = null;               //When this reaches 0 then it switches over to Game Over screen
                            PlayerKilledSoundInstance.Play();
                        }
                    }
                       //Second array
                       if (AlienMissileFired2 != null)
                       {
                           Rectangle rectAlienMissile2 = new Rectangle((int)AlienMissileFired2.GetPosition().X, (int)AlienMissileFired2.GetPosition().Y, AlienMissileImg2.Width, AlienMissileImg2.Height);
                           Rectangle rectRocket = new Rectangle(RocketXPos, 700, RocketLauncherImg.Width, RocketLauncherImg.Height);

                           if (rectAlienMissile2.Intersects(rectRocket))
                           {
                               if (AmountofLives == 0)
                               {
                                   AlienMissileFired2 = null;
                                   GameState = 4;
                                   PlayerKilledSoundInstance.Play();
                               }
                               AmountofLives = AmountofLives - 1;
                               AlienMissileFired2 = null;
                               PlayerKilledSoundInstance.Play();
                           }
                       }
                       //Third array
                       if (AlienMissileFired3 != null)
                       {
                           Rectangle rectAlienMissile3 = new Rectangle((int)AlienMissileFired3.GetPosition().X, (int)AlienMissileFired3.GetPosition().Y, AlienMissileImg2.Width, AlienMissileImg2.Height);
                           Rectangle rectRocket = new Rectangle(RocketXPos, 700, RocketLauncherImg.Width, RocketLauncherImg.Height);

                           if (rectAlienMissile3.Intersects(rectRocket))
                           {
                               if (AmountofLives == 0)
                               {
                                   AlienMissileFired3 = null;
                                   GameState = 4;
                                   PlayerKilledSoundInstance.Play();
                               }
                               AmountofLives = AmountofLives - 1;
                               AlienMissileFired3 = null;
                               PlayerKilledSoundInstance.Play();
                           }
                       }
                       //Fourth array
                       if (AlienMissileFired4 != null)
                       {
                           Rectangle rectAlienMissile4 = new Rectangle((int)AlienMissileFired4.GetPosition().X, (int)AlienMissileFired4.GetPosition().Y, AlienMissileImg2.Width, AlienMissileImg2.Height);
                           Rectangle rectRocket = new Rectangle(RocketXPos, 700, RocketLauncherImg.Width, RocketLauncherImg.Height);

                           if (rectAlienMissile4.Intersects(rectRocket))
                           {
                               if (AmountofLives == 0)
                               {
                                   AlienMissileFired4 = null;
                                   GameState = 4;
                                   PlayerKilledSoundInstance.Play();
                               }
                               AmountofLives = AmountofLives - 1;
                               AlienMissileFired4 = null;
                               PlayerKilledSoundInstance.Play();
                           }
                       }
            //Boss Missile
                       if (BossMissileFired != null)
                       {
                           Rectangle rectBossMissile = new Rectangle((int)BossMissileFired.GetPosition().X, (int)BossMissileFired.GetPosition().Y, BossMissileImg.Width, BossMissileImg.Height);
                           Rectangle rectRocket = new Rectangle(RocketXPos, 700, RocketLauncherImg.Width, RocketLauncherImg.Height);

                           if (rectBossMissile.Intersects(rectRocket))
                           {
                               if (AmountofLives == 0)
                               {
                                   BossMissileFired = null;
                                   GameState = 4;
                                   PlayerKilledSoundInstance.Play();
                               }
                               AmountofLives = AmountofLives - 1;
                               BossMissileFired = null;
                               PlayerKilledSoundInstance.Play();
                           }
                       }
           
            //Controls missle being fired
            //Checks whether missile intersects with Boss
                       if (MissileFired != null)
                       {
                           Rectangle rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg.Width, MissileImg.Height);
                           //Creates a rectangle in order to compare whether the missile intercepts with alien
                           if (BossAlien != null)
                           {
                               Rectangle rectBossInvader = new Rectangle(BossAlien.GetXPos(), BossAlien.GetYPos(), BossInvaderImg.Width, BossInvaderImg.Height);

                               if (rectMissile.Intersects(rectBossInvader))
                               {
                                   BossLife = BossLife - 1;
                                   MissileFired = null;
                                   PlayerScore = PlayerScore + 1000;    //Killing boss gives more score
                                   ExplosionSoundInstance.Play();

                                   if (BossLife == 0)
                                   {
                                       BossAlien = null;
                                       WeaponsCount = 1;    //Also gives a power up (Weapon upgrade)
                                       PlayerScore = PlayerScore + 1000;
                                       BosskilledSoundInstance.Play();
                                       ExplosionSoundInstance.Play();
                                   }
                               }
                           }
                       }

            // Checks whether the missle intersects with Alien
            if (MissileFired != null)
            {
                Rectangle rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg.Width, MissileImg.Height);

                for (int Count = 0; Count < 11; Count++)  //Rectangle is created to check whether both object intesect each other
                {
                    if (Invaders[Count] != null)         //If it does then the alien is declared as null (destroyed)
                    {
                        if (BossAlien == null)
                        {
                            rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg2.Width, MissileImg2.Height);
                        }
                        Rectangle rectInvader = new Rectangle(Invaders[Count].GetXPos(), Invaders[Count].GetYPos(), InvaderImg.Width, InvaderImg.Height);

                        if (rectMissile.Intersects(rectInvader))
                        {
                            Invaders[Count] = null;
                            MissileFired = null;

                            PlayerScore = PlayerScore + 150;

                            ExplosionSoundInstance.Play();
                            InvaderkilledSoundInstance.Play();

                            break;
                        }
                    }
                    //Second Array
                    if (InvadersOne[Count] != null)
                    {
                        if (BossAlien == null)
                        {
                            rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg2.Width, MissileImg2.Height);
                        }
                        Rectangle rectInvader = new Rectangle(InvadersOne[Count].GetXPos(), InvadersOne[Count].GetYPos(), InvaderImg.Width, InvaderImg.Height);

                        if (rectMissile.Intersects(rectInvader))
                        {

                            InvadersOne[Count] = null;
                            MissileFired = null;

                            PlayerScore = PlayerScore + 100;

                            ExplosionSoundInstance.Play();
                            InvaderkilledSoundInstance.Play();
                            break;
                        }
                    }
                    //Third Array
                    if (InvadersTwo[Count] != null)
                    {
                        if (BossAlien == null)
                        {
                            rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg2.Width, MissileImg2.Height);
                        }
                        Rectangle rectInvader = new Rectangle(InvadersTwo[Count].GetXPos(), InvadersTwo[Count].GetYPos(), InvaderImg.Width, InvaderImg.Height);

                        if (rectMissile.Intersects(rectInvader))
                        {
                            InvadersTwo[Count] = null;
                            MissileFired = null;

                            PlayerScore = PlayerScore + 50;

                            ExplosionSoundInstance.Play();
                            InvaderkilledSoundInstance.Play();
                            break;
                        }
                    }
                    //Fourth Array
                    if (InvadersThree[Count] != null)
                    {
                        if (BossAlien == null)
                        {
                            rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg2.Width, MissileImg2.Height);
                        }
                        Rectangle rectInvader = new Rectangle(InvadersThree[Count].GetXPos(), InvadersThree[Count].GetYPos(), InvaderImg.Width, InvaderImg.Height);

                        if (rectMissile.Intersects(rectInvader))
                        {
                            InvadersThree[Count] = null;
                            MissileFired = null;

                            PlayerScore = PlayerScore + 25;

                            ExplosionSoundInstance.Play();
                            InvaderkilledSoundInstance.Play();
                            break;
                        }
                    }
                    //Fifth Array
                    if (InvadersFour[Count] != null)
                    {
                        if (BossAlien == null)
                        {
                            rectMissile = new Rectangle((int)MissileFired.GetPosition().X, (int)MissileFired.GetPosition().Y, MissileImg2.Width, MissileImg2.Height);
                        }
                        Rectangle rectInvader = new Rectangle(InvadersFour[Count].GetXPos(), InvadersFour[Count].GetYPos(), InvaderImg.Width, InvaderImg.Height);

                        if (rectMissile.Intersects(rectInvader))
                        {
                            InvadersFour[Count] = null;
                            MissileFired = null;

                            PlayerScore = PlayerScore + 25;

                            ExplosionSoundInstance.Play();
                            InvaderkilledSoundInstance.Play();
                            break;
                        }
                    }
                }
            }
        }

        //Update method for End screen when you complete the level
        public void UpdateEnded(GameTime currentTime)
        {
            StarwarsSoundInstance.Stop();
            VictorySoundInstance.Play();
            //Exits game
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                VictorySoundInstance.Stop();
                this.Exit();
            }
            //Restarts game (Back to Start screen)
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                InitialiseGameVariables();
                GameState = 1;
                VictorySoundInstance.Stop();
            }

        }

        //Update Method for end screen when you lose
        public void GameoverUpdate(GameTime currentTime)
        {
            StarwarsSoundInstance.Stop();
            GameOverSoundInstance.Play();
            //Exits game
            if (Keyboard.GetState().IsKeyDown(Keys.X))
            {
                GameOverSoundInstance.Stop();
                this.Exit();
            }
            //Restarts game (Back to Start screen)
            if (Keyboard.GetState().IsKeyDown(Keys.R))
            {
                GameOverSoundInstance.Stop();
                InitialiseGameVariables();
                GameState = 1;
            }
        }

        //Update method for Help screen showing player controls
        public void HelpUpdate(GameTime currentTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                IntroSoundInstance.Stop();
                StarwarsSoundInstance.Play();
                InitialiseGameVariables();
                GameState = 2;
            }
        }

        //Update method for different gamestates
        //Case statement for delaring different Gamestates
        protected override void Update(GameTime gameTime)
        {
            switch (GameState)
            {
                case 1: UpdateStarted(gameTime);
                    break;

                case 2: UpdatePlaying(gameTime);
                    break;

                case 3: UpdateEnded(gameTime);
                    break;
                case 4: GameoverUpdate(gameTime);
                    break;
                case 5: HelpUpdate(gameTime);
                    break;
            }
            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        //Draw method for home screen
        public void DrawStarted(GameTime currentTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(Homescreenimg, Vector2.Zero, Color.White);     //Loads background

            Vector2 StringDimensions = GameFont.MeasureString("S P A C E   W A R S!");  //Displays text

            int XPos = (1024 - (int)StringDimensions.X) / 2;                            //X position of text

            StringDimensions = GameFont.MeasureString("P R E S S   'E N T E R'   T O    B E G I N");

            XPos = (1024 - (int)StringDimensions.X) / 2;

            spriteBatch.DrawString(GameFont, "P R E S S   'E N T E R'   T O    S T A R T", new Vector2(XPos, 400), Color.Yellow);   //X and Y position of text

            spriteBatch.End();
        }

        public void DrawPlaying(GameTime currentTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(StarfieldImg, Vector2.Zero, Color.White);
            spriteBatch.Draw(RocketLauncherImg, new Vector2(RocketXPos, 650), Color.White);

            //Drawing Player missile
            if (BossAlien == null & WeaponsCount == 1)  //Checks the conditions of Boss alien (Whether it has been destroyed)
            {                                           // and the weapon count to decide which bullet to draw
                if (MissileFired != null)               
                {
                    Vector2 MissilePos = new Vector2(MissileFired.GetPosition().X-10, MissileFired.GetPosition().Y - MissileImg2.Height);
                    spriteBatch.Draw(MissileImg2, MissilePos, Color.White);
                }
            }
            if (BossAlien == null & WeaponsCount == 2)   //This allows power ups when certain conditions are met
            {
                if (MissileFired != null)
                {
                    Vector2 MissilePos = new Vector2(MissileFired.GetPosition().X + 12, MissileFired.GetPosition().Y - MissileImg3.Height);
                    spriteBatch.Draw(MissileImg3, MissilePos, Color.White);
                }
            }
            //Second missile        
            if (BossAlien != null & WeaponsCount == 1)
            {
                if (MissileFired != null)
                {
                    Vector2 MissilePos = new Vector2(MissileFired.GetPosition().X+12, MissileFired.GetPosition().Y - MissileImg.Height);
                    spriteBatch.Draw(MissileImg, MissilePos, Color.White);
                }
            }
            if (BossAlien != null & WeaponsCount == 2)
            {
                if (MissileFired != null)
                {
                    Vector2 MissilePos = new Vector2(MissileFired.GetPosition().X + 12, MissileFired.GetPosition().Y - MissileImg3.Height);
                    spriteBatch.Draw(MissileImg3, MissilePos, Color.White);
                }
            }

            //Drawing Alien Missile
            //First Array
            if (AlienMissileFired != null) //Draws multiple bullets to allow multiple enemies shooting
            {
                Vector2 AlienMissilePos = new Vector2(AlienMissileFired.GetPosition().X +10, AlienMissileFired.GetPosition().Y+40 - AlienMissileImg.Height);
                spriteBatch.Draw(AlienMissileImg, AlienMissilePos, Color.White);
            }
            //Second Array
            if (AlienMissileFired2 != null)
            {
                Vector2 AlienMissile2Pos = new Vector2(AlienMissileFired2.GetPosition().X + 10, AlienMissileFired2.GetPosition().Y + 40 - AlienMissileImg2.Height);
                spriteBatch.Draw(AlienMissileImg2, AlienMissile2Pos, Color.White);
            }
            //Third Array
            if (AlienMissileFired3 != null)
            {
                Vector2 AlienMissile3Pos = new Vector2(AlienMissileFired3.GetPosition().X + 10, AlienMissileFired3.GetPosition().Y + 40 - AlienMissileImg.Height);
                spriteBatch.Draw(AlienMissileImg, AlienMissile3Pos, Color.White);
            }
            //Fourth Array
            if (AlienMissileFired4 != null)
            {
                Vector2 AlienMissile4Pos = new Vector2(AlienMissileFired4.GetPosition().X + 10, AlienMissileFired4.GetPosition().Y + 40 - AlienMissileImg2.Height);
                spriteBatch.Draw(AlienMissileImg2, AlienMissile4Pos, Color.White);
            }
            //Boss Missile
            if (BossMissileFired != null)
            {
                Vector2 BossMissilePos = new Vector2(BossMissileFired.GetPosition().X , BossMissileFired.GetPosition().Y  - BossMissileImg.Height);
                spriteBatch.Draw(BossMissileImg, BossMissilePos, Color.White);
            }

            //Drawing Boss Alien
            if (BossAlien != null)  //Display multiple lines of aliens if they have not been destroyed
            {                       //the if statement checks this
                spriteBatch.Draw(BossInvaderImg, BossAlien.GetPos(), Color.White);
            }
            //First Array
            for (int Count = 0; Count < 11; Count++)
            {
                if (Invaders[Count] != null)
                {
                    spriteBatch.Draw(InvaderImg, Invaders[Count].GetPos(), Color.White);
                }
            }
            //Second Array
            for (int Count = 0; Count < 11; Count++)
            {
                if (InvadersOne[Count] != null)
                {
                    spriteBatch.Draw(InvaderImg2, InvadersOne[Count].GetPos(), Color.White);
                }
            }
            //Third Array
            for (int Count = 0; Count < 11; Count++)
            {
                if (InvadersTwo[Count] != null)
                {
                    spriteBatch.Draw(InvaderImg3, InvadersTwo[Count].GetPos(), Color.White);
                }
            }
            //Fourth Array
            for (int Count = 0; Count < 11; Count++)
            {
                if (InvadersThree[Count] != null)
                {
                    spriteBatch.Draw(InvaderImg4, InvadersThree[Count].GetPos(), Color.White);
                }
            }
            //Fifth Array
            for (int Count = 0; Count < 11; Count++)
            {
                if (InvadersFour[Count] != null)
                {
                    spriteBatch.Draw(InvaderImg4, InvadersFour[Count].GetPos(), Color.White);
                }
            }

            string ScoreText = String.Format("Score = {0}", PlayerScore);       //Updates the player score as aliens get destroyed
            spriteBatch.DrawString(GameFont, ScoreText, new Vector2(10, 10), Color.White);

            string Lives = String.Format("Lives = {0}", AmountofLives); //Updates the amount of lives, if it reaches 0 then Game Over
            spriteBatch.DrawString(GameFont, Lives, new Vector2(900, 10), Color.White);

            if (BossAlien != null) //Write Boss lives
            {
                string BossLives = String.Format("Boss Life = {0}", BossLife); //Updates the amount of lives the Boss has, if it reaches 0, Boss = null
                spriteBatch.DrawString(GameFont, BossLives, new Vector2(863, 40), Color.Red);
            }
            if (BossAlien != null)  //Then write nothing
            {
            }
        
            if ((WeaponsCount == 1) & (BossAlien != null))  //Draws different missiles depending on the conditions set
            {
                string Weapon = String.Format("Weapon : Missile");
                spriteBatch.DrawString(GameFont, Weapon, new Vector2(10, 50), Color.White);//E.g. whether the boss alien has been destroyed will effect the missile used
            }
            if ((WeaponsCount == 1) & (BossAlien == null))
            {
                string Weapon = String.Format("Weapon : Plasma Beam");
                spriteBatch.DrawString(GameFont, Weapon, new Vector2(10, 50), Color.White);
            }
            if (WeaponsCount == 2)
            {
                string Weapon = String.Format("Weapon : Gatling Gun");
                spriteBatch.DrawString(GameFont, Weapon, new Vector2(10, 50), Color.White);
            }
            if (BossAlien == null)
            {
                string Powerup = String.Format("Power Up : Missile & Speed Upgrade lvl 3");     //Displays the Powerup changes as the Boss alien is hit
                spriteBatch.DrawString(GameFont, Powerup, new Vector2(10, 80), Color.Red);
            }
             if (BossLife == 3)
             {
                 string Powerup = String.Format("Power Up : None");                             //If the Boss is not hit then it diplays nothing
                 spriteBatch.DrawString(GameFont, Powerup, new Vector2(10, 80), Color.Red);
             }
             if (BossLife == 2)
             {
                 string Powerup = String.Format("Power Up : Speed Upgrade lvl 1");
                 spriteBatch.DrawString(GameFont, Powerup, new Vector2(10, 80), Color.Red);
             }
             if (BossLife == 1)
             {
                 string Powerup = String.Format("Power Up : Speed Upgrade lvl 2");
                 spriteBatch.DrawString(GameFont, Powerup, new Vector2(10, 80), Color.Red);
             }
            spriteBatch.End();
        }

        public void DrawEnded(GameTime currentTime) //This is the draw method for the Victory screen when the player wins the game
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(Endscreenimg, Vector2.Zero, Color.White);
            string FinalScoreString = String.Format("Final score = {0}", PlayerScore);  //Shows the final score

            Vector2 StringDimensions = GameFont.MeasureString(FinalScoreString);

            int XPos = (1024 - (int)StringDimensions.X) / 2;

            spriteBatch.DrawString(GameFont, FinalScoreString, new Vector2(XPos, 300), Color.White);

            StringDimensions = GameFont.MeasureString("P R E S S   'R'   T O    R E S T A R T   G A M E");  //Allows restarting or exiting ingame

            XPos = (1024 - (int)StringDimensions.X) / 2;

            spriteBatch.DrawString(GameFont, "P R E S S   'R'   T O    R E S T A R T   G A M E", new Vector2(XPos, 400), Color.White);

            StringDimensions = GameFont.MeasureString("P R E S S   'X'   T O    E X I T   G A M E");

            XPos = (1024 - (int)StringDimensions.X) / 2;

            spriteBatch.DrawString(GameFont, "P R E S S   'X'   T O    E X I T   G A M E", new Vector2(XPos, 500), Color.White);//X and Y position of each text
            spriteBatch.End();
        }

        //Draw method for Gameover end screen
        public void Gameoverdraw(GameTime currentTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(GameOverimg, Vector2.Zero, Color.White);
            string FinalScoreString = String.Format("Final score = {0}", PlayerScore); //Shows the final score
            Vector2 StringDimensions = GameFont.MeasureString(FinalScoreString);
            int XPos = (1024 - (int)StringDimensions.X) / 2;
            spriteBatch.DrawString(GameFont, FinalScoreString, new Vector2(XPos, 350), Color.White);
            StringDimensions = GameFont.MeasureString("P R E S S   'R'   T O    R E S T A R T   G A M E");
            XPos = (1024 - (int)StringDimensions.X) / 2;
            spriteBatch.DrawString(GameFont, "P R E S S   'R'   T O    R E S T A R T   G A M E", new Vector2(XPos, 500), Color.White); //X and Y position of each text
            XPos = (1024 - (int)StringDimensions.X) / 2;
            spriteBatch.DrawString(GameFont, "P R E S S   'X'   T O    E X I T   G A M E", new Vector2(XPos, 600), Color.White);
            spriteBatch.End();
        }
        //Draw method for helpscreen
        public void Helpdraw(GameTime currentTime)
        {
              spriteBatch.Begin();
              spriteBatch.Draw(HelpBackgroundimg, Vector2.Zero, Color.White);
              string FinalScoreString = String.Format("");
              Vector2 StringDimensions = GameFont.MeasureString(FinalScoreString);
              int XPos = (1024 - (int)StringDimensions.X) / 2;
              spriteBatch.DrawString(GameFont, "P R E S S  'S'  T O  S T A R T", new Vector2(350, 600), Color.White); //X and Y position of each text
              XPos = (1024 - (int)StringDimensions.X) / 2;
            spriteBatch.End();
        }
        
        //Draw method for each type of gamestate
        protected override void Draw(GameTime gameTime)
        {
            switch (GameState) //Case statement allows switching of gamestates between each case
            {
                case 1: DrawStarted(gameTime);
                    break;

                case 2: DrawPlaying(gameTime);
                    break;

                case 3: DrawEnded(gameTime);
                    break;
                case 4: Gameoverdraw (gameTime);
                    break;
                case 5: Helpdraw(gameTime);
                    break;
            }
            base.Draw(gameTime);
        }
    }
}
