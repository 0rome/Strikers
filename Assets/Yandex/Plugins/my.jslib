mergeInto(LibraryManager.library, {
  ShowAdv: function () {
    ysdk.adv.showFullscreenAdv({
      callbacks: {
        onClose: function (wasShown) {
          SendMessage('Yandex', 'TimeGo', 'Hello from JavaScript!');
        },
        onError: function (error) {
          SendMessage('Yandex', 'TimeGo', 'Hello from JavaScript!');
        },
      },
    });
  },

  ShowRewardedAdv: function () {
    ysdk.adv.showRewardedVideo({
      callbacks: {
        onOpen: function () {
          console.log('Video ad open.');
        },
        onRewarded: function () {
          console.log('Rewarded!');
          SendMessage('Yandex', 'GetReward', 'Hello from JavaScript!');
          SendMessage('Yandex', 'TimeGo', 'Hello from JavaScript!');
        },
        onClose: function () {
          console.log('Video ad closed.');
          SendMessage('Yandex', 'TimeGo', 'Hello from JavaScript!');
        },
        onError: function (e) {
          console.log('Error while open video ad:', e);
          SendMessage('Yandex', 'TimeGo', 'Hello from JavaScript!');
        },
      },
    });
  },

  GameReady: function () {
    YaGames.init()
      .then(function (ysdk) {
        if (ysdk.features && ysdk.features.LoadingAPI) {
          ysdk.features.LoadingAPI.ready();
        }
      })
      .catch(function (error) {
        console.error(error);
      });
  },

  GameStart: function () {
    YaGames.init()
      .then(function (ysdk) {
        if (ysdk.features && ysdk.features.GameplayAPI) {
          ysdk.features.GameplayAPI.start();
        }
      })
      .catch(function (error) {
        console.error(error);
      });
  },

  GameStop: function () {
    YaGames.init()
      .then(function (ysdk) {
        if (ysdk.features && ysdk.features.GameplayAPI) {
          ysdk.features.GameplayAPI.stop();
        }
      })
      .catch(function (error) {
        console.error(error);
      });
  },
});