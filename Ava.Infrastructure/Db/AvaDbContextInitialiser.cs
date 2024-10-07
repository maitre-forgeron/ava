namespace Ava.Infrastructure.Db;

public class AvaDbContextInitialiser
{
    private readonly ILogger<AvaDbContext> _logger;
    private readonly AvaDbContext _context;

    public AvaDbContextInitialiser(ILogger<AvaDbContext> logger, AvaDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralErrorMessages.DatabaseInitializationErrorMessage);
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralErrorMessages.DatabaseSeedingErrorMessage);
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        //TODO Seed here
    }
}
