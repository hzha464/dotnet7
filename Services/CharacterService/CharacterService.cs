global using AutoMapper;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace learn_.net7.Services.CharacterService
{
  public class CharacterService : IcharacterService
  {
    private readonly IMapper mapper;
    private readonly DataContext context;

    public CharacterService(IMapper mapper, DataContext context)
    {
      this.context = context;
      this.mapper = mapper;
    }
    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var character = mapper.Map<Character>(newCharacter);
        context.Characters.Add(character);
        await context.SaveChangesAsync();
        serviceResponse.Data = await context.Characters.Select(c=> mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

      try{

        var character = await context.Characters.FirstAsync(c=> c.Id == id);
        if(character is null){
          throw new Exception($"Character with Id  `{id}` not found.");
        }
        context.Characters.Remove(character);
        await context.SaveChangesAsync();
        serviceResponse.Data = await context.Characters.Select(c=> mapper.Map<GetCharacterDto>(c)).ToListAsync();
      }
      catch(Exception ex){
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var allCharacters = await context.Characters.ToListAsync();
        serviceResponse.Data = allCharacters.Select(c=> mapper.Map<GetCharacterDto>(c)).ToList();

      return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var character = await context.Characters.FirstOrDefaultAsync(c=>c.Id== id);
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
        if(character is not null){
            return serviceResponse;
        }else{
            throw new Exception("character not found");
        }

    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
      var serviceResponse = new ServiceResponse<GetCharacterDto>();

      try{

        var character = await context.Characters.FirstOrDefaultAsync(c=> c.Id == updateCharacter.Id);
        if(character is null){
          throw new Exception($"Character with Id  `{updateCharacter.Id}` not found.");
        }
        character.Name = updateCharacter.Name;
        character.HitPoints = updateCharacter.HitPoints;
        character.Intelligence = updateCharacter.Intelligence;
        character.GetRpgClass = updateCharacter.GetRpgClass;
        character.Strength = updateCharacter.Strength;
        character.Defense = updateCharacter.Defense;

        serviceResponse.Data = mapper.Map<GetCharacterDto>(character);
      }
      catch(Exception ex){
        serviceResponse.Success = false;
        serviceResponse.Message = ex.Message;
      }
        await context.SaveChangesAsync();
        return serviceResponse;
    }
  }
}