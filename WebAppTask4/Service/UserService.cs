using Microsoft.AspNetCore.Mvc;
using WebAppTask4.Models;

namespace WebAppTask4.Service
{
    
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            this._context = context;
        }

        private enum UserAction
        {
            Block, UnBlock, Delete
        }

        public async Task DeleteUser(string[] guids)
        {
            await ExecuteUserAction(UserAction.Delete, guids);
            await _context.SaveChangesAsync();
        }

        public async Task UnBlockUser(string[] guids)
        {
            await ExecuteUserAction(UserAction.UnBlock, guids);
            await _context.SaveChangesAsync();
        }

        public async Task BlockUser(string[] guids)
        {
            await ExecuteUserAction(UserAction.Block, guids);
            await _context.SaveChangesAsync();
        }

        private async Task ExecuteUserAction(UserAction action, string[] guids)
        {
            foreach (var guid in guids)
            {
                var user = await _context.Users.FindAsync(guid);

                if (user != null)
                {
                    switch(action)
                    {
                        case UserAction.Delete:
                            _context.Users.Remove(user);
                            break;
                        case UserAction.Block:
                            user.IsActive = false;
                            break;
                        case UserAction.UnBlock:
                            user.IsActive = true;
                            break;
                    }
                }
            }
        }
    }
}
