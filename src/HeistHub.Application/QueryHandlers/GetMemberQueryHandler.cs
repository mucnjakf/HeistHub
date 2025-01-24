using HeistHub.Application.Dtos;
using HeistHub.Application.Mappers;
using HeistHub.Application.Queries;
using HeistHub.Application.Repositories;
using HeistHub.Core.Entities;
using MediatR;

namespace HeistHub.Application.QueryHandlers;

public sealed class GetMemberQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetMemberQuery, MemberDto>
{
    public async Task<MemberDto> Handle(GetMemberQuery query, CancellationToken cancellationToken)
    {
        Member member = await memberRepository.GetAsync(query.MemberId);

        return member.ToMemberDto();
    }
}