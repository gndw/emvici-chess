using System;
using System.Collections;
using Agate.MVC.Core;
using UnityEngine;

namespace Agate.Chess.Request.Controller
{
    public class RequestController : GlobalController<RequestController>
    {
        [Inject]
        private ScheduleController _scheduleController { get; set; }

        public void FetchSavedNumber(Action<int> onFetchCompleted)
        {
            _scheduleController.StartCoroutine(IE_FetchSavedNumber(onFetchCompleted));
        }

        private IEnumerator IE_FetchSavedNumber(Action<int> onFetchCompleted)
        {
            yield return new WaitForSeconds(1);
            onFetchCompleted(10);
        }

    }
}