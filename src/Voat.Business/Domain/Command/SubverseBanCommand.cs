#region LICENSE

/*
    
    Copyright(c) Voat, Inc.

    This file is part of Voat.

    This source file is subject to version 3 of the GPL license,
    that is bundled with this package in the file LICENSE, and is
    available online at http://www.gnu.org/licenses/gpl-3.0.txt;
    you may not use this file except in compliance with the License.

    Software distributed under the License is distributed on an
    "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express
    or implied. See the License for the specific language governing
    rights and limitations under the License.

    All Rights Reserved.

*/

#endregion LICENSE

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voat.Data;

namespace Voat.Domain.Command
{
    public class SubverseBanCommand : CacheCommand<CommandResponse<bool?>, bool?>, IExcutableCommand<CommandResponse<bool?>>
    {
        private string _userToBan;
        private string _subverse;
        private string _reason;
        private bool? _force;

        public SubverseBanCommand(string userToBan, string subverse, string reason, bool? force = null)
        {
            _userToBan = userToBan;
            _subverse = subverse;
            _reason = reason;
            _force = force;
        }

        protected override async Task<Tuple<CommandResponse<bool?>, bool?>> CacheExecute()
        {
            using (var repo = new Repository(User))
            {
                var result = await repo.BanUserFromSubverse(_userToBan, _subverse, _reason, _force);
                return Tuple.Create(result, result.Response);
            }
        }

        protected override void UpdateCache(bool? result)
        {
            if (result.HasValue)
            {
                if (result.Value)
                {
                    //user has been banned
                }
                else
                {
                    //user has been unbanned
                }
            }
        }
    }
}
