using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using SCKRM.Json;
using SCKRM.Language;
using SCKRM.NBS;
using SCKRM.ProjectSetting;
using SCKRM.SaveLoad;
using SCKRM.Sound;
using SCKRM.Threads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace SCKRM.Resource
{
    public static class ResourceManager
    {
        [ProjectSettingSaveLoad]
        public sealed class Data
        {
            [JsonProperty] public static List<string> nameSpaces { get; set; } = new List<string>() { "sc-krm", "minecraft", "school-live" };
        }

        [GeneralSaveLoad]
        public sealed class SaveData
        {
            static List<string> _resourcePacks = new List<string>() { Kernel.streamingAssetsPath };
            [JsonProperty] public static List<string> resourcePacks
            {
                get
                {
                    if (_resourcePacks != null)
                    {
                        if (_resourcePacks.Count > 0)
                            _resourcePacks[_resourcePacks.Count - 1] = Kernel.streamingAssetsPath;
                        else
                            _resourcePacks.Add(Kernel.streamingAssetsPath);
                    }

                    return _resourcePacks;
                }
                set
                {
                    _resourcePacks = value;

                    if (_resourcePacks != null)
                    {
                        if (_resourcePacks.Count > 0)
                            _resourcePacks[_resourcePacks.Count - 1] = Kernel.streamingAssetsPath;
                        else
                            _resourcePacks.Add(Kernel.streamingAssetsPath);
                    }
                }
            }
            [JsonProperty] public static List<string> nameSpaces { get; set; } = new List<string>();
        }



        public const string defaultNameSpace = "sc-krm";

        public const string assetsPath = "assets/%NameSpace%";
        public const string texturePath = "assets/%NameSpace%/textures";
        public const string soundPath = "assets/%NameSpace%/sounds";
        public const string nbsPath = "assets/%NameSpace%/nbs";
        public const string languagePath = "assets/%NameSpace%/lang";
        public const string settingsPath = "projectSettings";

        static List<string> _nameSpaces = new List<string>();
#if UNITY_EDITOR
        public static List<string> nameSpaces
        {
            get
            {
                if (_nameSpaces.Count > 0)
                    return _nameSpaces;
                else
                    return Data.nameSpaces;
            }
        }
#else
        public static List<string> nameSpaces => _nameSpaces;
#endif

        public static string[] textureExtension { get; } = new string[] { "tga", "targa", "dds", "png", "jpg", "jif", "jpeg", "jpe", "bmp", "exr", "gif", "hdr", "iff", "pict", "tif", "tiff", "psd", "ico", "jng", "koa", "lbm", "mng", "pbm", "pcd", "pcx", "pgm", "ppm", "ras", "wbpm", "cut", "xbm", "xpm", "g3", "sgi", "j2k", "j2c", "jp2", "pfm", "webp", "jxr" };
        public static string[] textExtension { get; } = new string[] { "txt", "html", "htm", "xml", "bytes", "json", "csv", "yaml", "fnt" };
        public static string[] audioExtension { get; } = new string[] { "ogg", "mp3", "mp2", "wav", "aif", "xm", "mod", "it", "vag", "xma", "s3m" };
        public static string[] videoExtension { get; } = new string[] { "asf", "avi", "dv", "m4v", "mov", "mp4", "mpg", "mpeg", "ogv", "vp8", "webm", "wmv" };



        /// <summary>
        /// Texture2D = allTextures[nameSpace][type];
        /// </summary>
        static Dictionary<string, Dictionary<string, Texture2D>> packTextures { get; } = new Dictionary<string, Dictionary<string, Texture2D>>();
        /// <summary>
        /// Rect = allTextureRects[nameSpace][type][fileName];
        /// </summary>
        static Dictionary<string, Dictionary<string, Dictionary<string, Rect>>> packTextureRects { get; } = new Dictionary<string, Dictionary<string, Dictionary<string, Rect>>>();
        /// <summary>
        /// string = allTexturePaths[nameSpace][type][fileName];
        /// </summary>
        static Dictionary<string, Dictionary<string, Dictionary<string, string>>> packTexturePaths { get; } = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        /// <summary>
        /// string = allTexturePaths[nameSpace][type];
        /// </summary>
        static Dictionary<string, Dictionary<string, string>> packTextureTypePaths { get; } = new Dictionary<string, Dictionary<string, string>>();
        /// <summary>
        /// Sprite = allTextureSprites[nameSpace][type][fileName];
        /// </summary>
        static Dictionary<string, Dictionary<string, Dictionary<string, Sprite[]>>> allTextureSprites { get; } = new Dictionary<string, Dictionary<string, Dictionary<string, Sprite[]>>>();



        /// <summary>
        /// SoundData = allSounds[nameSpace][key];
        /// </summary>
        static Dictionary<string, Dictionary<string, SoundData<SoundMetaData>>> allSounds { get; } = new Dictionary<string, Dictionary<string, SoundData<SoundMetaData>>>();
        /// <summary>
        /// NBSData = allNBSs[nameSpace][key];
        /// </summary>
        static Dictionary<string, Dictionary<string, SoundData<NBSMetaData>>> allNBS { get; } = new Dictionary<string, Dictionary<string, SoundData<NBSMetaData>>>();



        /// <summary>
        /// string = allLanguages[nameSpace][language][key];
        /// </summary>
        static Dictionary<string, Dictionary<string, Dictionary<string, string>>> allLanguages { get; } = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();



        static List<UnityEngine.Object> garbages = new List<UnityEngine.Object>();



        public static bool isInitialLoadPackTexturesEnd { get; private set; } = false;
        public static bool isInitialLoadSpriteEnd { get; private set; } = false;
        public static bool isInitialLoadAudioEnd { get; private set; } = false;
        public static bool isAudioReset { get; private set; } = false;
        public static bool isInitialLoadLanguageEnd { get; private set; } = false;
        public static bool isResourceRefesh { get; private set; } = false;



        public static event Action audioResetEnd;



        public static AsyncTask resourceRefreshAsyncTask = null;
        public static AsyncTask resourceRefreshDetailedAsyncTask = null;

        /// <summary>
        /// ????????? ???????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Resource refresh (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static async UniTask ResourceRefresh(bool garbageRemoval)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(ResourceRefresh));
            if (!Kernel.isPlaying)
                throw new NotPlayModeMethodException(nameof(ResourceRefresh));
            if (isResourceRefesh)
                return;

            _nameSpaces = Data.nameSpaces.Union(SaveData.nameSpaces).ToList();

            isResourceRefesh = true;

            resourceRefreshAsyncTask = new AsyncTask("notice.running_task.resource_pack_refresh.name", "", false, true);
            resourceRefreshAsyncTask.progress = 0;
            resourceRefreshAsyncTask.maxProgress = 4;

            try
            {
                Debug.Log("ResourceManager: Resource refresh start!");
                Debug.Log("ResourceManager: Waiting for pack textures to set...");
                
                resourceRefreshDetailedAsyncTask = new AsyncTask("notice.running_task.resource_pack_refresh.set_pack_textures.name", "", false, true);

                await SetPackTextures();

                resourceRefreshAsyncTask.progress = 1;

                resourceRefreshDetailedAsyncTask.Remove(true);
                resourceRefreshDetailedAsyncTask = null;

                if (!Kernel.isPlaying)
                    return;

                Debug.Log("ResourceManager: Waiting for sprite to set...");

                resourceRefreshDetailedAsyncTask = new AsyncTask("notice.running_task.resource_pack_refresh.set_sprite.name", "", false, true);

                await SetSprite();

                resourceRefreshAsyncTask.progress = 2;

                resourceRefreshDetailedAsyncTask.Remove(true);
                resourceRefreshDetailedAsyncTask = null;

                if (!Kernel.isPlaying)
                    return;

                Debug.Log("ResourceManager: Waiting for language to set...");

                resourceRefreshDetailedAsyncTask = new AsyncTask("notice.running_task.resource_pack_refresh.set_language.name", "", false, true);

                await SetLanguage();

                isInitialLoadLanguageEnd = true;

                resourceRefreshAsyncTask.progress = 3;

                resourceRefreshDetailedAsyncTask.Remove(true);
                resourceRefreshDetailedAsyncTask = null;

                if (!Kernel.isPlaying)
                    return;

                Debug.Log("ResourceManager: Waiting for audio to set...");

                resourceRefreshDetailedAsyncTask = new AsyncTask("notice.running_task.resource_pack_refresh.set_audio.name", "", false, true);

                await SetAudio();

                resourceRefreshAsyncTask.progress = 4;

                resourceRefreshDetailedAsyncTask.Remove(true);
                resourceRefreshDetailedAsyncTask = null;

                if (!Kernel.isPlaying)
                    return;

                Debug.Log("ResourceManager: Resource refresh finished!");
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                Debug.LogError("ResourceManager: Resource refresh failed");

                if (!InitialLoadManager.isInitialLoadEnd)
                    throw;
            }

            resourceRefreshAsyncTask.progress = resourceRefreshAsyncTask.maxProgress;
            resourceRefreshAsyncTask.Remove(true);
            resourceRefreshAsyncTask = null;

            if (garbageRemoval)
            {
                GarbageRemoval();
                GC.Collect();
            }

            isResourceRefesh = false;
        }

        /// <summary>
        /// ??????????????? ???????????? ?????? ????????? ???????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Combine all textures from resource packs into one (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        static async UniTask SetPackTextures()
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(SetPackTextures));
            if (!Kernel.isPlaying)
                throw new NotPlayModeMethodException(nameof(SetPackTextures));

            foreach (var item in packTextures)
                foreach (var item2 in item.Value)
                    garbages.Add(item2.Value);

            packTextures.Clear();
            packTextureRects.Clear();
            packTexturePaths.Clear();
            packTextureTypePaths.Clear();
            Dictionary<string, Dictionary<string, Texture2D[]>> nameSpace_type_textures = new Dictionary<string, Dictionary<string, Texture2D[]>>();
            Dictionary<string, Dictionary<string, List<string>>> nameSpace_type_textureNames = new Dictionary<string, Dictionary<string, List<string>>>();

            resourceRefreshDetailedAsyncTask.maxProgress = SaveData.resourcePacks.Count * nameSpaces.Count;

            //?????? ??????????????? ??????????????????
            for (int i = 0; i < SaveData.resourcePacks.Count; i++)
            {
                string resourcePack = SaveData.resourcePacks[i];
                //????????? ????????????????????? ?????????????????? (??????????????? ?????? ????????????????????? ???????????? ????????????!)
                for (int j = 0; j < nameSpaces.Count; j++)
                {
                    string nameSpace = nameSpaces[j];
                    string resourcePackTexturePath = PathTool.Combine(resourcePack, texturePath.Replace("%NameSpace%", nameSpace));

                    if (!Directory.Exists(resourcePackTexturePath))
                        continue;

                    string[] types;
                    try
                    {
                        types = Directory.GetDirectories(resourcePackTexturePath, "*", SearchOption.AllDirectories);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        Debug.LogError($"Exception occurred while trying to find the type of resource pack texture");
                        Debug.LogError($"Texture type path where the exception occurred: {resourcePackTexturePath}");

                        continue;
                    }

                    //???????????? ?????? ?????? ?????? ????????? ?????? ????????? ?????????????????? (?????? ?????? ?????? ????????? ???????????? ??????????????? ??? ?????? ????????? "assets/sc-krm/textures/asdf/asdf2/asdf3.png" ????????? ????????? "asdf/asdf2"??? ?????????)
                    for (int k = 0; k < types.Length; k++)
                    {
                        string typePath = types[k].Replace("\\", "/");
                        string type = typePath.Substring(resourcePackTexturePath.Length + 1, typePath.Length - resourcePackTexturePath.Length - 1);
                        List<Texture2D> textures = new List<Texture2D>();
                        List<string> textureNames = new List<string>();
                        Dictionary<string, string> fileName_texturePaths = new Dictionary<string, string>();
                        TextureMetaData textureMetaData = JsonManager.JsonRead<TextureMetaData>(typePath + ".json", true);
                        if (textureMetaData == null)
                            textureMetaData = new TextureMetaData();

                        List<string> paths = new List<string>();

                        for (int l = 0; l < textureExtension.Length; l++)
                        {
                            string[] files;
                            try
                            {
                                files = Directory.GetFiles(typePath, "*." + textureExtension[l]);
                            }
                            catch (Exception e)
                            {
                                Debug.LogException(e);
                                Debug.LogError($"An exception occurred while locating the texture file in the resource pack texture type folder.");
                                Debug.LogError($"Texture path where the exception occurred: {typePath}");

                                continue;
                            }

                            paths.AddRange(files);
                        }

                        if (paths.Count <= 0)
                            continue;

                        //?????? ?????? ?????? ?????? ???????????? ?????????????????? (?????? ?????? ?????? ?????? ??????... ???????????? ???????????? ???????????? ????????? ???????????? ????????????)
                        for (int l = 0; l < paths.Count; l++)
                        {
                            string path = paths[l].Replace("\\", "/");
                            Texture2D texture = await GetTextureAsync(path, true, textureMetaData);

                            if (!Kernel.isPlaying)
                                return;

                            if (textureNames.Contains(texture.name))
                                continue;
                            
                            if (!nameSpace_type_textureNames.ContainsKey(nameSpace) || !nameSpace_type_textureNames[nameSpace].ContainsKey(type))
                            {
                                fileName_texturePaths.Add(texture.name, path);
                                textureNames.Add(texture.name);
                                textures.Add(texture);
                            }
                            else
                            {
                                //?????? ?????????????????? ?????? ???????????? ???????????????, ????????? ???????????? ???????????????
                                if (!nameSpace_type_textureNames[nameSpace][type].Contains(texture.name))
                                {
                                    fileName_texturePaths.Add(texture.name, path);
                                    textureNames.Add(texture.name);
                                    textures.Add(texture);
                                }
                            }
                        }
                        
                        if (!packTextureTypePaths.ContainsKey(nameSpace))
                        {
                            packTextureTypePaths.Add(nameSpace, new Dictionary<string, string>());
                            packTextureTypePaths[nameSpace].Add(type, typePath);
                        }
                        else if (!packTextureTypePaths[nameSpace].ContainsKey(type))
                            packTextureTypePaths[nameSpace].Add(type, typePath);

                        if (!packTexturePaths.ContainsKey(nameSpace))
                        {
                            packTexturePaths.Add(nameSpace, new Dictionary<string, Dictionary<string, string>>());
                            packTexturePaths[nameSpace].Add(type, fileName_texturePaths);
                        }
                        else if (!packTexturePaths[nameSpace].ContainsKey(type))
                            packTexturePaths[nameSpace].Add(type, fileName_texturePaths);
                        else
                            packTexturePaths[nameSpace][type] = packTexturePaths[nameSpace][type].Concat(fileName_texturePaths).ToDictionary(a => a.Key, b => b.Value);

                        if (!nameSpace_type_textureNames.ContainsKey(nameSpace))
                        {
                            nameSpace_type_textureNames.Add(nameSpace, new Dictionary<string, List<string>>());
                            nameSpace_type_textureNames[nameSpace].Add(type, textureNames);
                        }
                        else if (!nameSpace_type_textureNames[nameSpace].ContainsKey(type))
                            nameSpace_type_textureNames[nameSpace].Add(type, textureNames);
                        else
                            nameSpace_type_textureNames[nameSpace][type] = nameSpace_type_textureNames[nameSpace][type].Concat(textureNames).ToList();

                        if (!nameSpace_type_textures.ContainsKey(nameSpace))
                        {
                            nameSpace_type_textures.Add(nameSpace, new Dictionary<string, Texture2D[]>());
                            nameSpace_type_textures[nameSpace].Add(type, textures.ToArray());
                        }
                        else if (!nameSpace_type_textures[nameSpace].ContainsKey(type))
                            nameSpace_type_textures[nameSpace].Add(type, textures.ToArray());
                        else
                            nameSpace_type_textures[nameSpace][type] = nameSpace_type_textures[nameSpace][type].Concat(textures).ToArray();



                        if (await UniTask.DelayFrame(1, PlayerLoopTiming.Initialization, AsyncTaskManager.cancelToken).SuppressCancellationThrow())
                            return;
                    }

                    resourceRefreshDetailedAsyncTask.progress++;
                }
            }



            resourceRefreshDetailedAsyncTask.progress = 0;
            resourceRefreshDetailedAsyncTask.maxProgress = 0;
            foreach (var nameSpace in nameSpace_type_textures)
                resourceRefreshDetailedAsyncTask.maxProgress += nameSpace.Value.Count;

            foreach (var nameSpace in nameSpace_type_textures)
            {
                /*allTextureRects*/ Dictionary<string, Dictionary<string, Rect>> type_fileName = new Dictionary<string, Dictionary<string, Rect>>();
                /*allTextures*/ Dictionary<string, Texture2D> type_texture = new Dictionary<string, Texture2D>();
                foreach (var type in nameSpace.Value)
                {
                    if (!Kernel.isPlaying)
                        return;

                    Texture2D[] textures = type.Value;
                    Texture2D[] textures2 = new Texture2D[textures.Length];
                    string[] textureNames = new string[textures.Length];
                    int width = 0;
                    int height = 0;
                    for (int i = 0; i < textures.Length; i++)
                    {
                        Texture2D texture = textures[i];
                        textures2[i] = texture;
                        textureNames[i] = texture.name;
                        width += texture.width + 10;
                        height += texture.height + 10;

                        if (i == textures.Length - 1)
                        {
                            width -= 10;
                            height -= 10;
                        }
                    }
                    
                    /*allTextureRects*/
                    TextureMetaData textureMetaData = JsonManager.JsonRead<TextureMetaData>(packTextureTypePaths[nameSpace.Key][type.Key] + ".json", true);
                    if (textureMetaData == null)
                        textureMetaData = new TextureMetaData();

                    Texture2D background = new Texture2D(width, height);
                    Dictionary<string, Rect> fileName_rect = new Dictionary<string, Rect>();
                    
                    Rect[] rects = background.PackTextures(textures2, 10, int.MaxValue);
                    background.filterMode = textureMetaData.filterMode;

                    if (textureMetaData.compressionType == TextureMetaData.CompressionType.normal)
                        background.Compress(false);
                    else if (textureMetaData.compressionType == TextureMetaData.CompressionType.highQuality)
                        background.Compress(true);

                    for (int i = 0; i < rects.Length; i++)
                        fileName_rect.Add(textureNames[i], rects[i]);
                    type_fileName.Add(type.Key, fileName_rect);

                    /*allTextures*/
                    type_texture.Add(type.Key, background);

                    for (int j = 0; j < textures.Length; j++)
                    {
                        Texture2D texture = textures[j];
                        UnityEngine.Object.Destroy(texture);
                    }

                    if (await UniTask.DelayFrame(1, PlayerLoopTiming.Initialization, AsyncTaskManager.cancelToken).SuppressCancellationThrow())
                        return;

                    resourceRefreshDetailedAsyncTask.progress++;
                }
                /*allTextureRects*/ packTextureRects.Add(nameSpace.Key, type_fileName);
                /*allTextures*/ packTextures.Add(nameSpace.Key, type_texture);
            }

            isInitialLoadPackTexturesEnd = true;
        }

        /// <summary>
        /// ?????? ???????????? ?????? ?????????????????? ???????????? ???????????? ???????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Put all the combined textures into sprites and put them in the list (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotMainThreadMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        static async UniTask SetSprite()
        {
            if (!isInitialLoadPackTexturesEnd)
                throw new NotInitialLoadEndMethodException(nameof(SetSprite));
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(SetSprite));
            if (!Kernel.isPlaying)
                throw new NotPlayModeMethodException(nameof(SetSprite));

            foreach (var item in allTextureSprites)
                foreach (var item2 in item.Value)
                    foreach (var item3 in item2.Value)
                        for (int i = 0; i < item3.Value.Length; i++)
                            garbages.Add(item3.Value[i]);

            allTextureSprites.Clear();

            foreach (var nameSpace in packTextureRects)
                foreach (var type in nameSpace.Value)
                    resourceRefreshDetailedAsyncTask.maxProgress += type.Value.Count;

            foreach (var nameSpace in packTextureRects)
            {
                foreach (var type in nameSpace.Value)
                {
                    foreach (var fileName in type.Value)
                    {
                        if (!Kernel.isPlaying)
                            return;

                        Texture2D background = SearchPackTexture(type.Key, nameSpace.Key);
                        Rect rect = fileName.Value;
                        rect = new Rect(rect.x * background.width, rect.y * background.height, rect.width * background.width, rect.height * background.height);

                        SpriteMetaData[] spriteMetaDatas = JsonManager.JsonRead<SpriteMetaData[]>(SearchTexturePath(type.Key, fileName.Key, nameSpace.Key) + ".json", true);
                        if (spriteMetaDatas == null)
                            spriteMetaDatas = new SpriteMetaData[1];

                        for (int i = 0; i < spriteMetaDatas.Length; i++)
                        {
                            SpriteMetaData spriteMetaData = spriteMetaDatas[i];
                            if (spriteMetaData == null)
                            {
                                spriteMetaData = new SpriteMetaData();
                                spriteMetaData.RectMinMax(rect.width, rect.height);
                                spriteMetaDatas[i] = spriteMetaData;
                            }

                            spriteMetaDatas[i].rect = new JRect(rect.x + spriteMetaData.rect.x, rect.y + spriteMetaData.rect.y, rect.width - (rect.width - spriteMetaData.rect.width), rect.height - (rect.height - spriteMetaData.rect.height));
                        }
                        Sprite[] sprites = GetSprites(background, spriteMetaDatas);
                        
                        if (!allTextureSprites.ContainsKey(nameSpace.Key))
                        {
                            allTextureSprites.Add(nameSpace.Key, new Dictionary<string, Dictionary<string, Sprite[]>>());
                            allTextureSprites[nameSpace.Key].Add(type.Key, new Dictionary<string, Sprite[]>());
                            allTextureSprites[nameSpace.Key][type.Key].Add(fileName.Key, sprites);
                        }
                        else if (!allTextureSprites[nameSpace.Key].ContainsKey(type.Key))
                        {
                            allTextureSprites[nameSpace.Key].Add(type.Key, new Dictionary<string, Sprite[]>());
                            allTextureSprites[nameSpace.Key][type.Key].Add(fileName.Key, sprites);
                        }
                        else
                            allTextureSprites[nameSpace.Key][type.Key].Add(fileName.Key, sprites);

                        if (await UniTask.DelayFrame(1, PlayerLoopTiming.Initialization, AsyncTaskManager.cancelToken).SuppressCancellationThrow())
                            return;

                        resourceRefreshDetailedAsyncTask.progress++;
                    }
                }
            }

            isInitialLoadSpriteEnd = true;
        }

        /// <summary>
        /// ??????????????? sounds.json?????? ???????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Get audio from sounds.json in resource pack (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        static async UniTask SetAudio()
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(SetAudio));
            if (!Kernel.isPlaying)
                throw new NotPlayModeMethodException(nameof(SetAudio));

            foreach (var item in allSounds)
            {
                foreach (var item2 in item.Value)
                {
                    for (int i = 0; i < item2.Value.sounds.Length; i++)
                        garbages.Add(item2.Value.sounds[i].audioClip);
                }
            }

            allSounds.Clear();
            resourceRefreshDetailedAsyncTask.maxProgress = SaveData.resourcePacks.Count * nameSpaces.Count;
            for (int i = 0; i < SaveData.resourcePacks.Count; i++)
            {
                string resourcePack = SaveData.resourcePacks[i];
                for (int j = 0; j < nameSpaces.Count; j++)
                {
                    string nameSpace = nameSpaces[j];
                    string soundFolderPath = PathTool.Combine(resourcePack, soundPath.Replace("%NameSpace%", nameSpace));
                    string nbsFolderPath = PathTool.Combine(resourcePack, nbsPath.Replace("%NameSpace%", nameSpace));
                    
                    (bool success, bool cancel) = await TryGetSoundData(soundFolderPath, allSounds, soundMetaDataCreateFunc);
                    if (cancel)
                        return;

                    (success, cancel) = await TryGetSoundData(nbsFolderPath, allNBS, nbsMetaDataCreateFunc);
                    if (cancel)
                        return;

                    resourceRefreshDetailedAsyncTask.progress++;

                    async UniTask<SoundMetaData> soundMetaDataCreateFunc(string folderPath, SoundMetaData soundMetaData)
                    {
                        string audioPath = PathTool.Combine(folderPath, soundMetaData.path);
                        AudioClip audioClip = await GetAudio(audioPath, false, soundMetaData.stream);
                        if (!Kernel.isPlaying)
                            return null;

                        if (audioClip != null)
                            return new SoundMetaData(soundMetaData.path, soundMetaData.pitch, soundMetaData.tempo, soundMetaData.stream, soundMetaData.loopStartTime, audioClip);

                        return null;
                    }

                    async UniTask<NBSMetaData> nbsMetaDataCreateFunc(string folderPath, NBSMetaData nbsMetaData)
                    {
                        string soundPath = PathTool.Combine(nbsFolderPath, nbsMetaData.path);
                        if (!File.Exists(soundPath + ".nbs"))
                            return null;
                        
                        NBSFile nbsFile = NBSManager.ReadNBSFile(soundPath + ".nbs");
                        if (nbsFile != null)
                        {
                            await UniTask.DelayFrame(1);
                            return new NBSMetaData(nbsMetaData.path, nbsMetaData.pitch, nbsMetaData.tempo, nbsFile);
                        }

                        return null;
                    }

                    async UniTask<(bool success, bool cancel)> TryGetSoundData<MetaData>(string folderPath, Dictionary<string, Dictionary<string, SoundData<MetaData>>> allSounds, Func<string, MetaData, UniTask<MetaData>> metaDataCreateFunc) where MetaData : SoundMetaDataParent
                    {
                        if (Directory.Exists(folderPath))
                        {
                            Dictionary<string, SoundData<MetaData>> soundDatas = JsonManager.JsonRead<Dictionary<string, SoundData<MetaData>>>(folderPath + ".json", true);
                            if (soundDatas != null)
                            {
                                foreach (var soundData in soundDatas)
                                {
                                    if (soundData.Value.sounds == null)
                                        continue;

                                    List<MetaData> soundMetaDatas = new List<MetaData>();
                                    for (int k = 0; k < soundData.Value.sounds.Length; k++)
                                    {
                                        if (!Kernel.isPlaying)
                                            return (false, true);

                                        MetaData soundMetaData = soundData.Value.sounds[k];
                                        soundMetaData = await metaDataCreateFunc.Invoke(folderPath, soundMetaData);
                                        if (soundMetaData != null)
                                            soundMetaDatas.Add(soundMetaData);
                                    }

                                    if (!allSounds.ContainsKey(nameSpace))
                                    {
                                        allSounds.Add(nameSpace, new Dictionary<string, SoundData<MetaData>>());
                                        allSounds[nameSpace].Add(soundData.Key, new SoundData<MetaData>(soundData.Value.subtitle, soundData.Value.isBGM, soundMetaDatas.ToArray()));
                                    }
                                    else if (!allSounds[nameSpace].ContainsKey(soundData.Key))
                                        allSounds[nameSpace].Add(soundData.Key, new SoundData<MetaData>(soundData.Value.subtitle, soundData.Value.isBGM, soundMetaDatas.ToArray()));
                                }
                            }

                            return (true, false);
                        }

                        return (false, false);
                    }
                }
            }

            isInitialLoadAudioEnd = true;
        }

        /// <exception cref="NotPlayModeMethodException"></exception>
        static async UniTask SetLanguage()
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeMethodException(nameof(SetLanguage));

            allLanguages.Clear();

            LanguageManager.Language[] languages = LanguageManager.GetLanguages();
            resourceRefreshDetailedAsyncTask.maxProgress = SaveData.resourcePacks.Count * nameSpaces.Count * languages.Length;

            int l = 0;
            for (int i = 0; i < SaveData.resourcePacks.Count; i++)
            {
                string resourcePack = SaveData.resourcePacks[i];
                for (int j = 0; j < nameSpaces.Count; j++)
                {
                    string nameSpace = nameSpaces[j];
                    for (int k = 0; k < languages.Length; k++)
                    {
                        resourceRefreshDetailedAsyncTask.progress = l;
                        l++;

                        LanguageManager.Language language = languages[k];

                        Dictionary<string, string> dictionary = JsonManager.JsonRead<Dictionary<string, string>>(PathTool.Combine(resourcePack, languagePath.Replace("%NameSpace%", nameSpace), language.language) + ".json", true);
                        if (dictionary == null)
                            continue;

                        foreach (var languageDictionary in dictionary)
                        {
                            if (!allLanguages.ContainsKey(nameSpace))
                            {
                                allLanguages.Add(nameSpace, new Dictionary<string, Dictionary<string, string>>());
                                allLanguages[nameSpace].Add(language.language, new Dictionary<string, string>());
                                allLanguages[nameSpace][language.language].Add(languageDictionary.Key, languageDictionary.Value);
                            }
                            else if (!allLanguages[nameSpace].ContainsKey(language.language))
                            {
                                allLanguages[nameSpace].Add(language.language, new Dictionary<string, string>());
                                allLanguages[nameSpace][language.language].Add(languageDictionary.Key, languageDictionary.Value);
                            }
                            else if (!allLanguages[nameSpace][language.language].ContainsKey(languageDictionary.Key))
                                allLanguages[nameSpace][language.language].Add(languageDictionary.Key, languageDictionary.Value);
                        }

                        if (await UniTask.DelayFrame(1, PlayerLoopTiming.Initialization, AsyncTaskManager.cancelToken).SuppressCancellationThrow())
                            return;
                    }
                }
            }
        }


        /// <summary>
        /// ???????????? ???????????????
        /// delete garbage
        /// </summary>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static void GarbageRemoval()
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(SetAudio));

            for (int i = 0; i < garbages.Count; i++)
                UnityEngine.Object.DestroyImmediate(garbages[i]);

            garbages.Clear();
        }

        public static async UniTaskVoid AudioReset()
        {
            if (isAudioReset)
                return;

            isAudioReset = true;

            List<float> playersTime = new List<float>();
            for (int i = 0; i < SoundManager.soundList.Count; i++)
                playersTime.Add(SoundManager.soundList[i].time);

            AudioSettings.Reset(AudioSettings.GetConfiguration());

            AsyncTask asyncTask = new AsyncTask("notice.running_task.audio_refresh.name", "", false, true);
            resourceRefreshAsyncTask = asyncTask;
            resourceRefreshDetailedAsyncTask = asyncTask;

            await SetAudio();
            asyncTask.Remove(true);

            resourceRefreshAsyncTask = null;
            resourceRefreshDetailedAsyncTask = null;

            isAudioReset = false;
            audioResetEnd?.Invoke();

            SoundManager.SoundRefresh();
            GarbageRemoval();
            GC.Collect();

            for (int i = 0; i < SoundManager.soundList.Count; i++)
                SoundManager.soundList[i].time = playersTime[i];
        }


        #region Search Method
        /// <summary>
        /// ????????? ???????????? ????????? ????????? ???????????????
        /// Finds the path of the merged texture and returns it
        /// </summary>
        /// <param name="type">
        /// ??????
        /// Type
        /// </param>
        /// <param name="name">
        /// ??????
        /// Name
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static string SearchTexturePath(string type, string name, string nameSpace = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadPackTexturesEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchTexturePath));

            if (type == null)
                type = "";
            if (name == null)
                name = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (packTexturePaths.ContainsKey(nameSpace) && packTexturePaths[nameSpace].ContainsKey(type) && packTexturePaths[nameSpace][type].ContainsKey(name))
                return packTexturePaths[nameSpace][type][name];

            return "";
        }

        /// <summary>
        /// ????????? ???????????? ???????????????
        /// Returns the merged texture
        /// </summary>
        /// <param name="type">
        /// ??????
        /// Type
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static Texture2D SearchPackTexture(string type, string nameSpace = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadPackTexturesEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchPackTexture));

            if (type == null)
                type = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (packTextures.ContainsKey(nameSpace) && packTextures[nameSpace].ContainsKey(type))
                return packTextures[nameSpace][type];

            return null;
        }

        /// <summary>
        /// ????????? ???????????? Rect??? ???????????????
        /// Returns a Rect of the merged texture.
        /// </summary>
        /// <param name="type">
        /// ??????
        /// Type
        /// </param>
        /// <param name="name">
        /// ??????
        /// Name
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static Rect SearchTextureRect(string type, string name, string nameSpace = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadPackTexturesEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchTextureRect));

            if (type == null)
                type = "";
            if (name == null)
                name = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (packTextureRects.ContainsKey(nameSpace) && packTextureRects[nameSpace].ContainsKey(type) && packTextureRects[nameSpace][type].ContainsKey(name))
                return packTextureRects[nameSpace][type][name];

            return Rect.zero;
        }



        /// <summary>
        /// ??????????????? ??????????????? ?????????????????? ?????? ???????????????
        /// </summary>
        /// <param name="type">
        /// ??????
        /// Type
        /// </param>
        /// <param name="name">
        /// ??????
        /// Name
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static Sprite[] SearchSprites(string type, string name, string nameSpace = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadSpriteEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchSprites));

            if (type == null)
                type = "";
            if (name == null)
                name = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (allTextureSprites.ContainsKey(nameSpace))
            {
                if (allTextureSprites[nameSpace].ContainsKey(type))
                {
                    if (allTextureSprites[nameSpace][type].ContainsKey(name))
                        return allTextureSprites[nameSpace][type][name];
                }
            }
            return null;
        }

        /// <summary>
        /// ?????????????????? ????????? ???????????? ???????????????
        /// It can be executed even if it is not in the initial loading and play mode
        /// </summary>
        /// <param name="key">
        /// ??????
        /// Path
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static string SearchLanguage(string key, string nameSpace = "", string language = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadLanguageEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchLanguage));

            if (key == null)
                key = "";
            if (nameSpace == null)
                nameSpace = "";
            if (language == null)
                language = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;
            if (language == "")
                language = LanguageManager.SaveData.currentLanguage;

            if (allLanguages.ContainsKey(nameSpace))
            {
                if (allLanguages[nameSpace].ContainsKey(language))
                {
                    if (allLanguages[nameSpace][language].ContainsKey(key))
                        return allLanguages[nameSpace][language][key];
                }
            }

            return "";
        }

        /// <summary>
        /// ?????????????????? ????????? ???????????? ?????? ???????????????
        /// Finds and returns sound data from resource packs
        /// </summary>
        /// <param name="key">
        /// ???
        /// Key
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static SoundData<SoundMetaData> SearchSoundData(string key, string nameSpace = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadAudioEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchSoundData));

            if (key == null)
                key = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (allSounds.ContainsKey(nameSpace))
            {
                if (allSounds[nameSpace].ContainsKey(key))
                    return allSounds[nameSpace][key];
            }
            return null;
        }

        /// <summary>
        /// ?????????????????? NBS ???????????? ?????? ???????????????
        /// Finds and returns sound data from resource packs
        /// </summary>
        /// <param name="key">
        /// ???
        /// Key
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotInitialLoadEndMethodException"></exception>
        /// <exception cref="NotPlayModeMethodException"></exception>
        public static SoundData<NBSMetaData> SearchNBSData(string key, string nameSpace = "")
        {
            if (!Kernel.isPlaying)
                throw new NotPlayModeSearchMethodException();
            if (!isInitialLoadAudioEnd)
                throw new NotInitialLoadEndMethodException(nameof(SearchNBSData));

            if (key == null)
                key = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (allNBS.ContainsKey(nameSpace))
            {
                if (allNBS[nameSpace].ContainsKey(key))
                    return allNBS[nameSpace][key];
            }
            return null;
        }
        #endregion



        #region Get Resource Method

        #region Get Texture
        /// <summary>
        /// ????????? ????????? Texture2D ???????????? ???????????????
        /// Import image files as Texture2D type
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ??????
        /// </param>
        /// <returns></returns>
        public static Texture2D GetTexture(string path, bool pathExtensionUse = false, TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            if (!pathExtensionUse)
                FileExtensionExists(path, out path, textureExtension);

            TextureMetaData textureMetaData = JsonManager.JsonRead<TextureMetaData>(path + ".json", true);
            if (textureMetaData == null)
            {
                textureMetaData = new TextureMetaData();
                return GetTexture(path, true, textureMetaData.filterMode, textureMetaData.mipmapUse, textureMetaData.compressionType, textureFormat);
            }
            return GetTexture(path, true, FilterMode.Point, true, TextureMetaData.CompressionType.none, textureFormat);
        }

        /// <summary>
        /// ????????? ????????? Texture2D ???????????? ???????????????
        /// Import image files as Texture2D type
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ??????
        /// </param>
        /// <returns></returns>
        public static Texture2D GetTexture(string path, bool pathExtensionUse, TextureMetaData textureMetaData, TextureFormat textureFormat = TextureFormat.RGBA32) => GetTexture(path, pathExtensionUse, textureMetaData.filterMode, textureMetaData.mipmapUse, textureMetaData.compressionType, textureFormat);

        /// <summary>
        /// ????????? ????????? Texture2D ???????????? ???????????????
        /// Import image files as Texture2D type
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ?????? (png, jpg ??????????????? ??????)
        /// </param>
        /// <returns></returns>
        public static Texture2D GetTexture(string path, bool pathExtensionUse, FilterMode filterMode, bool mipmapUse, TextureMetaData.CompressionType compressionType, TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            if (path == null)
                path = "";

            bool exists;
            if (!pathExtensionUse)
                exists = FileExtensionExists(path, out path, textureExtension);
            else
                exists = File.Exists(path);
            
            if (exists)
            {
                Texture2D texture = new Texture2D(0, 0, textureFormat, mipmapUse);
                texture.filterMode = filterMode;
                texture.name = Path.GetFileName(path);

                AsyncImageLoader.LoaderSettings loaderSettings = AsyncImageLoader.LoaderSettings.Default;
                loaderSettings.generateMipmap = mipmapUse;
                loaderSettings.logException = true;

                if (!AsyncImageLoader.LoadImage(texture, File.ReadAllBytes(path), loaderSettings))
                    return null;

                if (compressionType == TextureMetaData.CompressionType.normal)
                    texture.Compress(false);
                else if (compressionType == TextureMetaData.CompressionType.highQuality)
                    texture.Compress(true);

                return texture;
            }

            return null;
        }
        #endregion

        #region Get Texture Async
        /// <summary>
        /// ????????? ????????? Texture2D ???????????? ???????????????
        /// Import image files as Texture2D type
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ?????? (png, jpg ??????????????? ??????)
        /// </param>
        /// <returns></returns>
        public static UniTask<Texture2D> GetTextureAsync(string path, bool pathExtensionUse = false, TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            if (!pathExtensionUse)
                FileExtensionExists(path, out path, textureExtension);

            TextureMetaData textureMetaData = JsonManager.JsonRead<TextureMetaData>(path + ".json", true);
            if (textureMetaData == null)
            {
                textureMetaData = new TextureMetaData();
                return GetTextureAsync(path, true, textureMetaData.filterMode, textureMetaData.mipmapUse, textureMetaData.compressionType, textureFormat);
            }
            return GetTextureAsync(path, true, FilterMode.Point, true, TextureMetaData.CompressionType.none, textureFormat);
        }

        /// <summary>
        /// ????????? ????????? Texture2D ???????????? ???????????? ???????????????
        /// ????????? ????????? ???????????? ????????? SC KRM??? ???????????? ????????? ???????????? ???????????? ????????? ????????????
        /// Asynchronously import an image file as a Texture2D type.
        /// Various formats are supported. Among them, there are formats supported by SC KRM and formats supported by Unity.
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ??????
        /// </param>
        /// <returns></returns>
        public static UniTask<Texture2D> GetTextureAsync(string path, bool pathExtensionUse, TextureMetaData textureMetaData, TextureFormat textureFormat = TextureFormat.RGBA32) => GetTextureAsync(path, pathExtensionUse, textureMetaData.filterMode, textureMetaData.mipmapUse, textureMetaData.compressionType, textureFormat);

        /// <summary>
        /// ????????? ????????? Texture2D ???????????? ???????????? ???????????????
        /// ????????? ????????? ???????????? ????????? SC KRM??? ???????????? ????????? ???????????? ???????????? ????????? ????????????
        /// Asynchronously import an image file as a Texture2D type.
        /// Various formats are supported. Among them, there are formats supported by SC KRM and formats supported by Unity.
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ??????
        /// </param>
        /// <returns></returns>
        public static async UniTask<Texture2D> GetTextureAsync(string path, bool pathExtensionUse, FilterMode filterMode, bool mipmapUse, TextureMetaData.CompressionType compressionType, TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(GetAudio));

            if (path == null)
                path = "";

            if (pathExtensionUse)
                path = PathTool.GetPathWithExtension(path);

            if (FileExtensionExists(path, out string outPath, textureExtension))
            {
                using UnityWebRequest www = UnityWebRequest.Get(outPath);
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                    Debug.LogError(www.error);

                Texture2D texture = new Texture2D(0, 0, textureFormat, mipmapUse);
                texture.filterMode = filterMode;
                texture.name = Path.GetFileName(path);

                AsyncImageLoader.LoaderSettings loaderSettings = AsyncImageLoader.LoaderSettings.Default;
                loaderSettings.generateMipmap = mipmapUse;
                loaderSettings.logException = true;

                if (!await AsyncImageLoader.LoadImageAsync(texture, www.downloadHandler.data, loaderSettings))
                    return null;

                if (compressionType == TextureMetaData.CompressionType.normal)
                    texture.Compress(false);
                else if (compressionType == TextureMetaData.CompressionType.highQuality)
                    texture.Compress(true);

                return texture;
            }

            return null;
        }
        #endregion

        #region Get Sprite
        /// <summary>
        /// ???????????? ?????????????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Convert texture to sprite (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Sprite GetSprite(Texture2D texture, SpriteMetaData spriteMetaData = null)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(GetSprite));

            if (texture == null)
                return null;

            if (spriteMetaData == null)
                return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100, 0, SpriteMeshType.FullRect, Vector4.zero);
            else
            {
                spriteMetaData.RectMinMax(texture.width, texture.height);
                spriteMetaData.PixelsPreUnitMinSet();

                return Sprite.Create(texture, spriteMetaData.rect, spriteMetaData.pivot, spriteMetaData.pixelsPerUnit, 0, SpriteMeshType.FullRect, spriteMetaData.border);
            }
        }

        /// <summary>
        /// ????????? ????????? ?????????????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????.)
        /// Import image files as sprites (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="resourcePackPath">
        /// ???????????? ??????
        /// Resource Pack Path
        /// </param>
        /// <param name="type">
        /// ??????
        /// Type
        /// </param>
        /// <param name="name">
        /// ??????
        /// Name
        /// </param>
        /// <param name="nameSpace">
        /// ??????????????????
        /// Name Space
        /// </param>
        /// <param name="textureFormat">
        /// ????????? ??????
        /// Texture Format
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Sprite[] GetSprites(string resourcePackPath, string type, string name, string nameSpace = "", TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(GetSprites));

            if (resourcePackPath == null || resourcePackPath == "")
                return null;
            if (type == null || type == "")
                return null;
            if (name == null || name == "")
                return null;
            if (nameSpace == null)
                nameSpace = null;

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            string path = PathTool.Combine(resourcePackPath, texturePath.Replace("%NameSpace%", nameSpace));
            string allPath = PathTool.Combine(path, type, name);
            
            TextureMetaData textureMetaData = JsonManager.JsonRead<TextureMetaData>(PathTool.Combine(path, type) + ".json", true);
            if (textureMetaData == null)
                textureMetaData = new TextureMetaData();

            Texture2D texture = GetTexture(allPath, false, textureMetaData, textureFormat);
            FileExtensionExists(allPath, out string allPath2, textureExtension);
            SpriteMetaData[] spriteMetaDatas = JsonManager.JsonRead<SpriteMetaData[]>(allPath2 + ".json", true);
            return GetSprites(texture, spriteMetaDatas);
        }

        /// <summary>
        /// ????????? ????????? ?????????????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????.)
        /// Import image files as sprites (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="path">
        /// ????????? ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Sprite[] GetSprites(string path, bool pathExtensionUse = false, TextureFormat textureFormat = TextureFormat.RGBA32)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(GetSprites));

            if (path == null)
                path = "";

            Texture2D texture = GetTexture(path, pathExtensionUse, textureFormat);
            SpriteMetaData[] spriteMetaDatas = JsonManager.JsonRead<SpriteMetaData[]>(path + ".json", true);
            return GetSprites(texture, spriteMetaDatas);
        }

        /// <summary>
        /// ???????????? ?????????????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Import image files as sprites (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="texture">
        /// ????????? ?????????
        /// texture to convert
        /// </param>
        /// <param name="spriteMetaDatas">
        /// Sprite's metadata
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Sprite[] GetSprites(Texture2D texture, params SpriteMetaData[] spriteMetaDatas)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(GetSprites));

            if (texture == null)
                return null;
            if (spriteMetaDatas == null)
                spriteMetaDatas = new SpriteMetaData[] { new SpriteMetaData() };

            Sprite[] sprites = new Sprite[spriteMetaDatas.Length];
            for (int i = 0; i < spriteMetaDatas.Length; i++)
            {
                SpriteMetaData spriteMetaData = spriteMetaDatas[i];
                if (spriteMetaData == null)
                    spriteMetaData = new SpriteMetaData();

                spriteMetaData.RectMinMax(texture.width, texture.height);
                spriteMetaData.PixelsPreUnitMinSet();

                Sprite sprite = Sprite.Create(texture, spriteMetaData.rect, spriteMetaData.pivot, spriteMetaData.pixelsPerUnit, 0, SpriteMeshType.FullRect, spriteMetaData.border);
                sprite.name = texture.name;

                sprites[i] = sprite;
            }
            return sprites;
        }
        #endregion



        /// <summary>
        /// ????????? ????????? string ???????????? ???????????????
        /// Import text file as string type
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <returns></returns>
        public static string GetText(string path, bool pathExtensionUse = false)
        {
            bool exists;
            if (!pathExtensionUse)
                exists = FileExtensionExists(path, out path, textExtension);
            else
                exists = File.Exists(path);

            if (exists)
                return File.ReadAllText(path);

            return "";
        }

        /// <summary>
        /// ????????? ????????? ????????? ???????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Import audio files as audio clips (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="path">
        /// ??????
        /// Path
        /// </param>
        /// <param name="pathExtensionUse">
        /// ????????? ????????? ??????
        /// Use extension in path
        /// </param>
        /// <param name="stream">
        /// ?????????
        /// Stream
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static async UniTask<AudioClip> GetAudio(string path, bool pathExtensionUse = false, bool stream = false)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(GetAudio));

            if (path == null)
                path = "";

            if (pathExtensionUse)
                path = PathTool.GetPathWithExtension(path);
                
            
            AudioClip audioClip = await getSound(".ogg", AudioType.OGGVORBIS);
            if (audioClip == null) audioClip = await getSound(".mp3", AudioType.MPEG);
            if (audioClip == null) audioClip = await getSound(".mp2", AudioType.MPEG);
            if (audioClip == null) audioClip = await getSound(".wav", AudioType.WAV);
            if (audioClip == null) audioClip = await getSound(".aif", AudioType.AIFF);
            if (audioClip == null) audioClip = await getSound(".xm", AudioType.XM);
            if (audioClip == null) audioClip = await getSound(".mod", AudioType.MOD);
            if (audioClip == null) audioClip = await getSound(".it", AudioType.IT);
            if (audioClip == null) audioClip = await getSound(".vag", AudioType.VAG);
            if (audioClip == null) audioClip = await getSound(".xma", AudioType.XMA);
            if (audioClip == null) audioClip = await getSound(".s3m", AudioType.S3M);

            return audioClip;

            async UniTask<AudioClip> getSound(string extension, AudioType type)
            {
                if (File.Exists(path + extension))
                {
                    using UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path + extension, type);
                    DownloadHandlerAudioClip downloadHandlerAudioClip = (DownloadHandlerAudioClip)www.downloadHandler;
                    downloadHandlerAudioClip.streamAudio = stream;
                    downloadHandlerAudioClip.compressed = false;

                    await www.SendWebRequest();

                    if (www.result != UnityWebRequest.Result.Success)
                        Debug.LogError(www.error);

                    return DownloadHandlerAudioClip.GetContent(www);
                }

                return null;
            }
        }
#endregion



        #region Get Key List
        /// <summary>
        /// ?????? ??? ????????? ??? ???????????? ??????????????? (????????? ?????? ????????????, ?????? ????????? ?????????????????? ?????? ?????????????????? ???????????? ????????????)
        /// Get the list of loaded audio keys (manually find in the default resource pack if not playing or not initially loaded)
        /// </summary>
        /// <param name="nameSpace">
        /// ????????? ??????????????????
        /// namespace to import
        /// </param>
        /// <returns></returns>
        public static string[] GetSoundDataKeys(string nameSpace = "")
        {
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (isInitialLoadAudioEnd)
            {
                if (allSounds.ContainsKey(nameSpace))
                    return allSounds[nameSpace].Keys.ToArray();
                else
                    return new string[0];
            }
            else
            {
                string path = PathTool.Combine(Kernel.streamingAssetsPath, soundPath.Replace("%NameSpace%", nameSpace));
                Dictionary<string, SoundData<SoundMetaData>> soundDatas = JsonManager.JsonRead<Dictionary<string, SoundData<SoundMetaData>>>(path + ".json", true);
                if (soundDatas != null)
                    return soundDatas.Keys.ToArray();
                else
                    return new string[0];
            }
        }
        /// <summary>
        /// ?????? ??? NBS ??? ???????????? ??????????????? (????????? ?????? ????????????, ?????? ????????? ?????????????????? ?????? ?????????????????? ???????????? ????????????)
        /// Get the list of loaded nbs keys (manually find in the default resource pack if not playing or not initially loaded)
        /// </summary>
        /// <param name="nameSpace">
        /// ????????? ??????????????????
        /// namespace to import
        /// </param>
        /// <returns></returns>
        public static string[] GetNBSDataKeys(string nameSpace = "")
        {
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (isInitialLoadAudioEnd)
            {
                if (allNBS.ContainsKey(nameSpace))
                    return allNBS[nameSpace].Keys.ToArray();
                else
                    return new string[0];
            }
            else
            {
                string path = PathTool.Combine(Kernel.streamingAssetsPath, nbsPath.Replace("%NameSpace%", nameSpace));
                Dictionary<string, SoundData<NBSMetaData>> soundDatas = JsonManager.JsonRead<Dictionary<string, SoundData<NBSMetaData>>>(path + ".json", true);
                if (soundDatas != null)
                    return soundDatas.Keys.ToArray();
                else
                    return new string[0];
            }
        }

        /// <summary>
        /// ?????? ??? ??????????????? ?????? ???????????? ??????????????? (????????? ?????? ????????????, ?????? ????????? ?????????????????? ?????? ?????????????????? ???????????? ????????????)
        /// Get the list of loaded sprite type (manually find in the default resource pack if not playing or not initially loaded)
        /// </summary>
        /// <param name="nameSpace">
        /// ????????? ??????????????????
        /// namespace to import
        /// </param>
        /// <returns></returns>
        public static string[] GetSpriteTypes(string nameSpace = "")
        {
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (isInitialLoadSpriteEnd)
            {
                if (allTextureSprites.ContainsKey(nameSpace))
                    return allTextureSprites[nameSpace].Keys.ToArray();
                else
                    return new string[0];
            }
            else
            {
                string resourcePackTexturePath = PathTool.Combine(Kernel.streamingAssetsPath, texturePath.Replace("%NameSpace%", nameSpace));
                string[] paths = Directory.GetDirectories(resourcePackTexturePath, "*", SearchOption.AllDirectories);
                for (int i = 0; i < paths.Length; i++)
                {
                    string path = paths[i];
                    paths[i] = path.Substring(resourcePackTexturePath.Length + 1, path.Length - resourcePackTexturePath.Length - 1).Replace("\\", "/");
                }

                return paths;
            }
        }
        /// <summary>
        /// ?????? ??? ??????????????? ??? ???????????? ??????????????? (????????? ?????? ????????????, ?????? ????????? ?????????????????? ?????? ?????????????????? ???????????? ????????????)
        /// Get the list of loaded sprite keys (manually find in the default resource pack if not playing or not initially loaded)
        /// </summary>
        /// <param name="nameSpace">
        /// ????????? ??????????????????
        /// namespace to import
        /// </param>
        /// <returns></returns>
        public static string[] GetSpriteKeys(string type, string nameSpace = "")
        {
            if (type == null)
                type = "";
            else if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (isInitialLoadSpriteEnd)
            {
                if (allTextureSprites.ContainsKey(nameSpace) && allTextureSprites[nameSpace].ContainsKey(type))
                    return allTextureSprites[nameSpace][type].Keys.ToArray();
                else
                    return new string[0];
            }
            else
            {
                string typePath = PathTool.Combine(Kernel.streamingAssetsPath, texturePath.Replace("%NameSpace%", nameSpace), type);

                if (!Directory.Exists(typePath))
                    return null;

                List<string> keys = new List<string>();
                for (int i = 0; i < textureExtension.Length; i++)
                {
                    string[] filePaths = Directory.GetFiles(typePath, "*." + textureExtension[i]);
                    for (int l = 0; l < filePaths.Length; l++)
                        filePaths[l] = Path.GetFileNameWithoutExtension(filePaths[l].Replace("\\", "/"));

                    keys.AddRange(filePaths);
                }

                return keys.Distinct().ToArray();
            }
        }

        /// <summary>
        /// ?????? ??? ?????? ??? ???????????? ??????????????? (????????? ?????? ????????????, ?????? ????????? ?????????????????? ?????? ?????????????????? ???????????? ????????????)
        /// Get the list of loaded language keys (manually find in the default resource pack if not playing or not initially loaded)
        /// </summary>
        /// <param name="nameSpace">
        /// ????????? ??????????????????
        /// namespace to import
        /// </param>
        /// <returns></returns>
        public static string[] GetLanguageKeys(string language, string nameSpace = "")
        {
            if (language == null)
                language = "";
            if (nameSpace == null)
                nameSpace = "";

            if (nameSpace == "")
                nameSpace = defaultNameSpace;

            if (isInitialLoadLanguageEnd)
            {
                if (allLanguages.ContainsKey(nameSpace) && allLanguages[nameSpace].ContainsKey(language))
                    return allLanguages[nameSpace][language].Keys.ToArray();
                else
                    return new string[0];
            }
            else
            {
                Dictionary<string, string> dictionary = JsonManager.JsonRead<Dictionary<string, string>>(PathTool.Combine(Kernel.streamingAssetsPath, languagePath.Replace("%NameSpace%", nameSpace), language) + ".json", true);
                if (dictionary != null)
                    return dictionary.Keys.ToArray();
                else
                    return new string[0];
            }
        }
        #endregion



        /// <summary>
        /// ???????????? ?????? ???????????? ????????? true??? ???????????????
        /// Returns true if files have a specific extension
        /// </summary>
        /// <param name="path">
        /// ????????? ??????
        /// Path
        /// </param>
        /// <param name="outPath">
        /// ????????? ???????????? ????????? ?????? ??????
        /// Full path including searched extension
        /// </param>
        /// <param name="extensions">
        /// ????????? ?????????
        /// extension list
        /// </param>
        /// <returns></returns>
        public static bool FileExtensionExists(string path, out string outPath, params string[] extensions)
        {
            if (path == null)
                path = "";
            if (extensions == null)
            {
                outPath = "";
                return false;
            }

            for (int i = 0; i < extensions.Length; i++)
            {
                string extension = extensions[i];
                if (File.Exists(path + "." + extension))
                {
                    outPath = path += "." + extension;
                    return true;
                }
            }

            outPath = "";
            return false;
        }

        /// <summary>
        /// ??????????????? ????????????????????? ???????????? ????????????????????? ???????????????.
        /// Detach a namespace from text and return the namespace.
        /// </summary>
        /// <param name="text">
        /// ????????? ?????????
        /// text to split
        /// </param>
        /// <param name="value">
        /// ???????????? ?????? ?????????
        /// Remaining Text
        /// </param>
        /// <returns></returns>
        public static string GetNameSpace(string text, out string value)
        {
            if (text == null)
            {
                value = "";
                return "";
            }

            if (text.Contains(":"))
            {
                int index = text.IndexOf(":");
                value = text.Substring(index + 1);
                return text.Remove(index);
            }
            else
            {
                value = text;
                return "";
            }
        }

        /// <summary>
        /// ??????????????? ????????? ????????? ???????????? ????????? ???????????????.
        /// Detach a namespace from text and return the namespace.
        /// </summary>
        /// <param name="text">
        /// ????????? ?????????
        /// text to split
        /// </param>
        /// <param name="value">
        /// ???????????? ?????? ?????????
        /// Remaining Text
        /// </param>
        /// <returns></returns>
        public static string GetTextureType(string text, out string value)
        {
            if (text == null)
            {
                value = "";
                return "";
            }

            if (text.Contains("/"))
            {
                int index = text.LastIndexOf("/");
                value = text.Substring(index + 1);
                return text.Remove(index);
            }
            else
            {
                value = text;
                return "";
            }
        }



        #region Texture Average Color
        /// <summary>
        /// ???????????? ?????? ????????? ???????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Gets the average color of a texture (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="texture">
        /// ?????????
        /// Texture
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Color AverageColorFromTexture(Texture2D texture) => AverageColorFromTexture(texture, 0, 0, texture.width, texture.height);

        /// <summary>
        /// ???????????? ?????? ????????? ???????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Gets the average color of a texture (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="texture">
        /// ?????????
        /// Texture
        /// </param>
        /// <param name="rect">
        /// ????????? ??????
        /// Texture Size
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Color AverageColorFromTexture(Texture2D texture, RectInt rect) => AverageColorFromTexture(texture, rect.x, rect.y, rect.width, rect.height);

        /// <summary>
        /// ???????????? ?????? ????????? ???????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Gets the average color of a texture (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="texture">
        /// ?????????
        /// Texture
        /// </param>
        /// <param name="x">
        /// X ??????
        /// X Pos
        /// </param>
        /// <param name="y">
        /// Y ??????
        /// Y Pos
        /// </param>
        /// <param name="width">
        /// ??????
        /// Width
        /// </param>
        /// <param name="height">
        /// ??????
        /// Height
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Color AverageColorFromTexture(Texture2D texture, int x, int y, int width, int height)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(AverageColorFromTexture));

            Color[] textureColors = texture.GetPixels(x, y, width, height);

            int length = textureColors.Length;

            float r = 0;
            float g = 0;
            float b = 0;
            float a = 0;

            for (int i = 0; i < length; i++)
            {
                Color color = textureColors[i];
                r += color.r;
                g += color.g;
                b += color.b;
                a += color.a;
            }

            return new Color(r / length, g / length, b / length, a / length);
        }
        #endregion

        #region Texture Color
        /// <summary>
        /// ????????? ???????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Convert color to texture (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="color">
        /// ??????
        /// Color
        /// </param>
        /// <param name="width">
        /// ??????
        /// Width
        /// </param>
        /// <param name="height">
        /// ??????
        /// Height
        /// </param>
        /// <param name="filterMode">
        /// ?????? ??????
        /// Filter Mode
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Texture2D TextureFromColor(Color color, int width, int height, FilterMode filterMode = FilterMode.Point)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(TextureFromColor));

            if (color == null)
                color = Color.white;
            if (width < 1)
                width = 1;
            if (height < 1)
                height = 1;

            Texture2D texture = new Texture2D(width, height, TextureFormat.DXT5, false);
            texture.filterMode = filterMode;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    texture.SetPixel(x, y, color);
            }
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// ????????? ???????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Convert color to texture (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="color">
        /// ??????
        /// Color
        /// </param>
        /// <param name="alpha">
        /// ?????? ?????????
        /// Alpha texture
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Texture2D TextureFromColor(Color color, Texture2D alpha)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(TextureFromColor));

            if (color == null)
                color = Color.white;

            Texture2D texture = new Texture2D(alpha.width, alpha.height, TextureFormat.DXT5, false);
            texture.filterMode = alpha.filterMode;
            for (int x = 0; x < alpha.width; x++)
            {
                for (int y = 0; y < alpha.height; y++)
                    texture.SetPixel(x, y, new Color(color.r, color.g, color.b, alpha.GetPixel(x, y).a));
            }
            texture.Apply();
            return texture;
        }

        /// <summary>
        /// ????????? ?????????????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Convert color to sprite (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="color">
        /// ??????
        /// Color
        /// </param>
        /// <param name="width">
        /// ??????
        /// Width
        /// </param>
        /// <param name="height">
        /// ??????
        /// Height
        /// </param>
        /// <param name="filterMode">
        /// ?????? ??????
        /// Filter Mode
        /// </param>
        /// <param name="spriteMetaData">
        /// ?????????????????? ?????? ?????????
        /// Sprite's metadata
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Sprite SpriteFromColor(Color color, int width = 1, int height = 1, FilterMode filterMode = FilterMode.Point, SpriteMetaData spriteMetaData = null)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(SpriteFromColor));

            if (color == null)
                color = Color.white;
            if (width < 1)
                width = 1;
            if (height < 1)
                height = 1;

            Texture2D texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
            texture.filterMode = filterMode;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    texture.SetPixel(x, y, color);
            }
            texture.Apply();
            return GetSprite(texture, spriteMetaData);
        }

        /// <summary>
        /// ????????? ?????????????????? ??????????????? (Unity API??? ???????????? ????????? ?????? ??????????????? ???????????? ?????????)
        /// Convert color to sprite (Since the Unity API is used, we need to run it on the main thread)
        /// </summary>
        /// <param name="color">
        /// ??????
        /// Color
        /// </param>
        /// <param name="alpha">
        /// ?????? ?????????
        /// Alpha
        /// </param>
        /// <param name="spriteMetaData">
        /// ?????????????????? ?????? ?????????
        /// Sprite's metadata
        /// </param>
        /// <returns></returns>
        /// <exception cref="NotMainThreadMethodException"></exception>
        public static Sprite SpriteFromColor(Color color, Texture2D alpha, SpriteMetaData spriteMetaData = null)
        {
            if (!ThreadManager.isMainThread)
                throw new NotMainThreadMethodException(nameof(SpriteFromColor));

            if (color == null)
                color = Color.white;

            Texture2D texture = new Texture2D(alpha.width, alpha.height, TextureFormat.RGBA32, false);
            texture.filterMode = alpha.filterMode;
            for (int x = 0; x < alpha.width; x++)
            {
                for (int y = 0; y < alpha.height; y++)
                    texture.SetPixel(x, y, new Color(color.r, color.g, color.b, alpha.GetPixel(x, y).a));
            }
            texture.Apply();
            return GetSprite(texture, spriteMetaData);
        }
        #endregion



        public static SoundData<MetaData> CreateSoundData<MetaData>(string subtitle, bool isBGM, MetaData sounds) where MetaData : SoundMetaDataParent
            => new SoundData<MetaData>(subtitle, isBGM, new MetaData[] { sounds });

        public static SoundData<MetaData> CreateSoundData<MetaData>(string subtitle, bool isBGM, MetaData[] sounds) where MetaData : SoundMetaDataParent
            => new SoundData<MetaData>(subtitle, isBGM, sounds);

        public static SoundMetaData CreateSoundMetaData(float pitch, float tempo, float loopStartTime, AudioClip audioClip)
        {
            if (audioClip == null)
                return null;

            if (audioClip.loadType == AudioClipLoadType.Streaming)
                return new SoundMetaData("", pitch, tempo, true, loopStartTime, audioClip);
            else
                return new SoundMetaData("", pitch, tempo, false, loopStartTime, audioClip);
        }

        public static NBSMetaData CreateNBSMetaData(float pitch, float tempo, NBSFile nbsFile)
        {
            if (nbsFile == null)
                return null;

            return new NBSMetaData("", pitch, tempo, nbsFile);
        }
    }

    public class TextureMetaData
    {
        [JsonProperty("Filter Mode")] public FilterMode filterMode = FilterMode.Point;
        [JsonProperty("Mipmap Use")] public bool mipmapUse = true;
        [JsonProperty("Compression")] public CompressionType compressionType = CompressionType.none;

        public enum CompressionType
        {
            none,
            normal,
            highQuality
        }
    }

    public class SpriteMetaData
    {
        [JsonProperty("Rect")] public JRect rect = new JRect(float.MinValue);
        [JsonProperty("Pivot")] public JVector2 pivot = new JVector2(0.5f);
        [JsonProperty("Pixels Per Unit")] public float pixelsPerUnit = 100;
        [JsonProperty("Border")] public JVector4 border = JVector4.zero;

        public void RectMinMax(float width, float height)
        {
            if (rect.x <= float.MinValue)
            {
                rect.x = 0;
                rect.y = 0;
                rect.width = width;
                rect.height = height;
            }

            if (rect.width < 1)
                rect.width = 1;
            if (rect.width > width)
                rect.width = width;
            if (rect.height < 1)
                rect.height = 1;
            if (rect.height > height)
                rect.height = height;

            if (rect.x < 0)
                rect.x = 0;
            if (rect.x > width - rect.width)
                rect.x = width - rect.width;
            if (rect.y < 0)
                rect.y = 0;
            if (rect.y > height - rect.height)
                rect.y = height - rect.height;
        }

        public void PixelsPreUnitMinSet()
        {
            if (pixelsPerUnit < 0.01f)
                pixelsPerUnit = 0.01f;
        }
    }



    public class SoundData<MetaData> where MetaData : SoundMetaDataParent
    {
        public SoundData(string subtitle, bool isBGM, params MetaData[] sounds)
        {
            this.subtitle = subtitle;
            this.isBGM = isBGM;
            this.sounds = sounds;
        }

        public string subtitle { get; } = "";
        public bool isBGM { get; } = false;
        public MetaData[] sounds { get; } = new MetaData[0];
    }

    public class SoundMetaDataParent
    {
        public SoundMetaDataParent(string path, float pitch, float tempo)
        {
            this.path = path;
            this.pitch = pitch;
            this.tempo = tempo;
        }

        public string path { get; } = "";
        public float pitch { get; } = 1;
        public float tempo { get; } = 1;
    }

    public class SoundMetaData : SoundMetaDataParent
    {
        public SoundMetaData(string path, float pitch, float tempo, bool stream, float loopStartTime, AudioClip audioClip) : base(path, pitch, tempo)
        {
            this.stream = stream;
            this.loopStartTime = loopStartTime;

            this.audioClip = audioClip;
        }

        public bool stream { get; } = false;
        public float loopStartTime { get; } = 0;

        [JsonIgnore] public AudioClip audioClip { get; }
    }

    public class NBSMetaData : SoundMetaDataParent
    {
        public NBSMetaData(string path, float pitch, float tempo, NBSFile nbsFile) : base(path, pitch, tempo) => this.nbsFile = nbsFile;

        [JsonIgnore] public NBSFile nbsFile { get; }
    }



    public class NotPlayModeSearchMethodException : Exception
    {
        /// <summary>
        /// Search function cannot be used outside of play mode
        /// ????????? ????????? ????????? Search ????????? ????????? ??? ????????????
        /// </summary>
        public NotPlayModeSearchMethodException() : base("Search function cannot be used outside of play mode\n????????? ????????? ????????? Search ????????? ????????? ??? ????????????") { }
    }
}