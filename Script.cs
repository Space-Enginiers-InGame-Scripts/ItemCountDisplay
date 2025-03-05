public Program() {
    Runtime.UpdateFrequency = UpdateFrequency.Update100; // Update every tick
}

public void Save() {
    // Optional: Save the state of the program if needed.
}

public void Main(string argument, UpdateType updateSource) {
    // Resource counters
    int ice = 0, stone = 0, scrap = 0, iron = 0, silicon = 0, nickel = 0, cobalt = 0, silver = 0, gold = 0, uranium = 0, magnesium = 0, platinum = 0;
    int ingotStone = 0, ingotIron = 0, ingotSilicon = 0, ingotNickel = 0, ingotCobalt = 0, ingotSilver = 0, ingotGold = 0, ingotUranium = 0, ingotMagnesium = 0, ingotPlatinum = 0;

    // Find LCD screen
    List<IMyTerminalBlock> lcdBlocks = new List<IMyTerminalBlock>();
    GridTerminalSystem.SearchBlocksOfName("ICE_LCD", lcdBlocks, block => block is IMyTextSurface);
    
    if (lcdBlocks.Count == 0) {
        throw new Exception("ICE_LCD not found!");
    }

    // Get all blocks with inventories
    List<IMyTerminalBlock> blocks = new List<IMyTerminalBlock>();
    GridTerminalSystem.GetBlocks(blocks);

    // Iterate through blocks and count resources
    foreach (var block in blocks) {
        if (block.HasInventory) {
            for (int i = 0; i < block.InventoryCount; i++) {
                var inventory = block.GetInventory(i);
                var items = new List<MyInventoryItem>();
                inventory.GetItems(items);

                foreach (var item in items) {
                    int amount = (int)item.Amount;
                    string itemType = item.Type.ToString();

                    // Count ores and ingots
                    if (itemType.Contains("Ore/Ice")) ice += amount;
                    else if (itemType.Contains("Ore/Stone")) stone += amount;
                    else if (itemType.Contains("Ore/Scrap")) scrap += amount;
                    else if (itemType.Contains("Ore/Iron")) iron += amount;
                    else if (itemType.Contains("Ore/Silicon")) silicon += amount;
                    else if (itemType.Contains("Ore/Nickel")) nickel += amount;
                    else if (itemType.Contains("Ore/Cobalt")) cobalt += amount;
                    else if (itemType.Contains("Ore/Silver")) silver += amount;
                    else if (itemType.Contains("Ore/Gold")) gold += amount;
                    else if (itemType.Contains("Ore/Uranium")) uranium += amount;
                    else if (itemType.Contains("Ore/Magnesium")) magnesium += amount;
                    else if (itemType.Contains("Ore/Platinum")) platinum += amount;
                    else if (itemType.Contains("Ingot/Stone")) ingotStone += amount;
                    else if (itemType.Contains("Ingot/Iron")) ingotIron += amount;
                    else if (itemType.Contains("Ingot/Silicon")) ingotSilicon += amount;
                    else if (itemType.Contains("Ingot/Nickel")) ingotNickel += amount;
                    else if (itemType.Contains("Ingot/Cobalt")) ingotCobalt += amount;
                    else if (itemType.Contains("Ingot/Silver")) ingotSilver += amount;
                    else if (itemType.Contains("Ingot/Gold")) ingotGold += amount;
                    else if (itemType.Contains("Ingot/Uranium")) ingotUranium += amount;
                    else if (itemType.Contains("Ingot/Magnesium")) ingotMagnesium += amount;
                    else if (itemType.Contains("Ingot/Platinum")) ingotPlatinum += amount;
                }
            }
        }
    }

    // Prepare output for LCD
    string panelText = "Resources (raw / ingot):\n";
    panelText += $"Ice: {ice}\n";
    panelText += $"Stone: {stone} / {ingotStone}\n";
    panelText += $"Iron: {iron} / {ingotIron}\n";
    panelText += $"Silicon: {silicon} / {ingotSilicon}\n";
    panelText += $"Nickel: {nickel} / {ingotNickel}\n";
    panelText += $"Cobalt: {cobalt} / {ingotCobalt}\n";
    panelText += $"Silver: {silver} / {ingotSilver}\n";
    panelText += $"Gold: {gold} / {ingotGold}\n";
    panelText += $"Uranium: {uranium} / {ingotUranium}\n";
    panelText += $"Magnesium: {magnesium} / {ingotMagnesium}\n";
    panelText += $"Platinum: {platinum} / {ingotPlatinum}\n";

    // Write to all found LCDs
    foreach (IMyTextSurface textSurface in lcdBlocks) {
        textSurface.WriteText(panelText);
    }
}
