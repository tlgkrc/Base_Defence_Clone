using Commands;
using Data.ValueObject.Base;
using Keys;
using Signals;
using UnityEngine;


namespace Managers
{
    public class SaveManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Private Variables
        
        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
        
        
        #endregion
        
        #endregion
        
        private void Awake()
        {
            Initialization();
        }
        
        private void Initialization()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand(); 
        }
        
        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            SaveSignals.Instance.onLevelSave += OnSaveLevel;
            SaveSignals.Instance.onLevelLoad += OnLevelLoad;
            
            // SaveSignals.Instance.onSaveAmmoWorkerData += _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadAmmoWorkerData += _loadGameCommand.Execute<AmmoWorkerData>;
            //
            // SaveSignals.Instance.onSaveMoneyWorkerData += _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadMoneyWorkerData += _loadGameCommand.Execute<MoneyWorkerData>;
            //
            // SaveSignals.Instance.onSaveMineBaseData += _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadMineBaseData += _loadGameCommand.Execute<MineBaseData>;
        
            SaveSignals.Instance.onSaveRoomData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadRoomData += _loadGameCommand.Execute<RoomData>;
        
            SaveSignals.Instance.onSaveTurretData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadTurretData += _loadGameCommand.Execute<TurretData>;
        
            // SaveSignals.Instance.onSaveForceFieldData += _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadForceFieldData += _loadGameCommand.Execute<ForceFieldData>;
        }
        
        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onLevelSave -= OnSaveLevel;
            SaveSignals.Instance.onLevelLoad -= OnLevelLoad;
            
            // SaveSignals.Instance.onSaveAmmoWorkerData -= _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadAmmoWorkerData -= _loadGameCommand.Execute<AmmoWorkerData>;
            //
            // SaveSignals.Instance.onSaveMoneyWorkerData -= _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadMoneyWorkerData -= _loadGameCommand.Execute<MoneyWorkerData>;
            //
            // SaveSignals.Instance.onSaveMineBaseData -= _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadMineBaseData -= _loadGameCommand.Execute<MineBaseData>;
            
            SaveSignals.Instance.onSaveRoomData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadRoomData -= _loadGameCommand.Execute<RoomData>;
            
            SaveSignals.Instance.onSaveTurretData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadTurretData -= _loadGameCommand.Execute<TurretData>;
            
            // SaveSignals.Instance.onSaveForceFieldData -= _saveGameCommand.Execute;
            // SaveSignals.Instance.onLoadForceFieldData -= _loadGameCommand.Execute<ForceFieldData>;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        
        private void OnSaveLevel()
        {
            SaveLevel(
                new LevelParams()
                {
                    Level = LevelSignals.Instance.onGetLevelID(),
                }
            );
        }
        
        private void SaveLevel(LevelParams saveDataParams)
        {
            ES3.Save("Level", saveDataParams.Level, "LevelData.es3");
        }
        
        private LevelParams OnLevelLoad()
        {
            print("OnLevelLoad");
            return new LevelParams()
            {
                Level = ES3.KeyExists("Level","LevelData.es3") ? ES3.Load<int>("Level","LevelData.es3") : 1,
            };
        }
 
    }
}