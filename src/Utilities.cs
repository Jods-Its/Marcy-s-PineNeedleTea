using UnityEngine;
using Il2Cpp;
using MelonLoader;
using UnityEngine.AddressableAssets;
using Il2CppTLD.Gear;
using Il2CppNodeCanvas.Tasks.Actions;





namespace PineNeedleTeaMod
{
    internal static class Utilities
    {
        public static GearItem teaReference = Addressables.LoadAssetAsync<GameObject>("GEAR_GreenTeaCup").WaitForCompletion().GetComponent<GearItem>();
        public static Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient> MakeVitaminC(int amount = 10)
        {
            FoodItem.Nutrient vitc = new FoodItem.Nutrient();
            vitc.m_Amount = amount;
            vitc.m_Nutrient = new Il2CppTLD.Gameplay.AssetReferenceNutrientDefinition("13a8bda1e12982e428b7551cc01b01df");
            Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient> list = new Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient>();
            list.Add(vitc);
            return list;
        }

        public static Material LiquidMaterial()
        {
            Texture2D LiquidTexture = Implementation.TeaTexturesBundle.LoadAsset<Texture2D>("T_PineTea_Cooking");
            Material LiquidMaterial = new Material(Addressables.LoadAssetAsync<GameObject>("GEAR_CoffeeCup").WaitForCompletion().GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        
        
        public static GameObject Needles = Addressables.LoadAssetAsync<GameObject>("GEAR_PineNeedle").WaitForCompletion();


        public static AssetBundle LoadFromStream(string name)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                MemoryStream? memory = new((int)stream.Length);
                stream!.CopyTo(memory);

                Il2CppSystem.IO.MemoryStream memoryStream = new Il2CppSystem.IO.MemoryStream(memory.ToArray());
                
                AssetBundle loadFromMemoryInternal = AssetBundle.LoadFromStream(memoryStream);
                return loadFromMemoryInternal;
            };
        }

        public static void SetDrinkItem(DrinkItem drinkItem, GearItem gearItem)
        {
            drinkItem.m_GearItem = gearItem;
            drinkItem.m_FoodItem = gearItem.m_FoodItem;
            drinkItem.m_LiquidType = teaReference.gameObject.GetComponent<DrinkItem>().m_LiquidType;
        }
        //Generic stuff 👇
        public static T? GetComponentSafe<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetComponentSafe<T>(component.GetGameObject());
        }
        public static T? GetComponentSafe<T>(this GameObject? gameObject) where T : Component
        {
            return gameObject == null ? default : gameObject.GetComponent<T>();
        }
        public static T? GetOrCreateComponent<T>(this Component? component) where T : Component
        {
            return component == null ? default : GetOrCreateComponent<T>(component.GetGameObject());
        }
        public static T? GetOrCreateComponent<T>(this GameObject? gameObject) where T : Component
        {
            if (gameObject == null)
            {
                return default;
            }

            T? result = GetComponentSafe<T>(gameObject);

            if (result == null)
            {
                result = gameObject.AddComponent<T>();
            }

            return result;
        }
        internal static GameObject? GetGameObject(this Component? component)
        {
            try
            {
                return component == null ? default : component.gameObject;
            }
            catch (System.Exception exception)
            {
                MelonLoader.MelonLogger.Msg($"Returning null since this could not obtain a Game Object from the component. Stack trace:\n{exception.Message}");
            }
            return null;
        }
    }
}


