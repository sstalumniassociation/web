export default defineProtectedEventHandler(event => ({
  id: event.context.params!.id,
  name: 'Qin Guan',
  memberId: 'ABC-1',
  graduationYear: 2024,
  memberType: 'exco',
}))
