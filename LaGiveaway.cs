string[] recepients = new[] {
  "Alentrachad",
  "Alysîa",
  "Asarya",
  "Azkî",
  "Beeper",
  "Bluetsunami",
  "Cardjester",
  "Dogz",
  "Dutzz",
  "Endashi",
  "Fabia",
  "Loxxx",
  "Lynu",
  "Minihatearcana",
  "Necrokingns",
  "Oxymyron",
  "Soulnami",
  "Turgut",
};

Item[] stash = new Item[] {
  // Skins.
  new() { Name = "Neon Weapon Selection Chest",                        Price = 22000,    Count = 3      },
  new() { Name = "Magic Bed Mount Selection Chest",                    Price = 13000,    Count = 4      },
  new() { Name = "Bike Mount Selection Chest",                         Price = 50000,    Count = 2      },
  new() { Name = "Midsummer Night's Dream Skin Selection Chest",       Price = 58999,    Count = 3      },
  new() { Name = "Hawk Topik Skin Selection Chest",                    Price = 50000,    Count = 2      },
  new() { Name = "Wingsuit Skin Selection Chest",                      Price = 50000,    Count = 3      },
  new() { Name = "Midsummer Night's Dream Weapon Selection Chest",     Price = 39998,    Count = 2      },
  new() { Name = "Lover Skin Chest",                                   Price = 125000,   Count = 3      },
  new() { Name = "Arkesia Weapon Selection Chest",                     Price = 10666,    Count = 2      },
  new() { Name = "Arkesia Fidelity Chest",                             Price = 24500,    Count = 1      },
  // These two aren't on the market. I pulled the price out of thin air.
  new() { Name = "Hawk Topik Weapon Selection Chest",                  Price = 20000,    Count = 2      },
  new() { Name = "Midsummer Night's Dream Instrument Selection Chest", Price = 5000,     Count = 1      },

  // Mats.
  new() { Name = "Supperior Oreha Fusion Material",                    Price = 21,       Count = 1096   },
  new() { Name = "Great Honor Leapstone",                              Price = 40,       Count = 1209   },
  new() { Name = "9999 x Crystallized Destruction Stone",              Price = 9999 * 3, Count = 26     },
  // Market is at 1g/stack but the supply is clearly outstripping demand. Half price it is.
  new() { Name = "9999 x Crystallized Guardian Stone",                 Price = 9999 / 2, Count = 39     },
  new() { Name = "Marvelous Honor Leapstone",                          Price = 57,       Count = 466    },
  new() { Name = "Obliteration Stone",                                 Price = 24,       Count = 3554   },
  new() { Name = "Protection Stone",                                   Price = 4,        Count = 5583   },
  new() { Name = "Honor Shard Pouch (S)",                              Price = 106,      Count = 3156   },
  new() { Name = "Honor Shard Pouch (M)",                              Price = 219,      Count = 1438   },
  new() { Name = "Honor Shard Pouch (L)",                              Price = 381,      Count = 841    },
  new() { Name = "Solar Grace",                                        Price = 32,       Count = 6350   },
  new() { Name = "Solar Blessing",                                     Price = 68,       Count = 2912   },
  new() { Name = "Solar Protection",                                   Price = 171,      Count = 1205   },
  
  // Gems.
  new() { Name = "Level 10 Annihilation Gem",                          Price = 499999,   Count = 8      },
  new() { Name = "Level 10 Crimson Flame Gem",                         Price = 204000,   Count = 11     },
  new() { Name = "Level 8 Crimson Flame Gem",                          Price = 42000,    Count = 1      },

  // Other.
  new() { Name = "Master Craft Kit",                                   Price = 33999,    Count = 1      },
  new() { Name = "Elemental HP Potion",                                Price = 27,       Count = 1655   },
  new() { Name = "Splendid Elemental HP Potion",                       Price = 68,       Count = 269    },
  new() { Name = "Gold",                                               Price = 1,        Count = 195488 },
};

const int MaxShortage = 1000;

int total = stash.Sum(x => x.Price * x.Count);
Array.Sort(stash, (a, b) => b.Price - a.Price);
RandomShuffle(recepients);

for (int i = 0; i != recepients.Length; ++i) {
  int received = 0;
  int remain = total / (recepients.Length - i);
  Console.WriteLine("{0}", recepients[i]);
  foreach (Item item in stash) {
    int n = Math.Min(remain / item.Price, item.Count);
    if (n == 0) continue;
    // A hack to spread out lvl 10 gems. Feels nice if everyone gets one.
    if (item.Name.StartsWith("Level 10") && i != recepients.Length - 1) n = 1;
    item.Count -= n;
    received += n * item.Price;
    remain -= n * item.Price;
    Console.WriteLine("  {0,7:N0} x {1}", n, item.Name);
    if (remain <= MaxShortage) break;
  }
  Console.WriteLine("    total = {0:N0} gold", received);
  Console.WriteLine();
  total -= received;
}

static void RandomShuffle<T>(IList<T> list) {
  Random rng = new(42069);
  int i = list.Count;
  while (i > 1) {
    i--;
    int j = rng.Next(i + 1);
    (list[i], list[j]) = (list[j], list[i]);
  }
}

class Item {
  public string Name { get; init; }
  public int Price { get; init; }
  public int Count { get; set; }
}
