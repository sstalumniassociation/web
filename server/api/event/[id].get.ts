import dayjs from 'dayjs'

export default defineProtectedEventHandler(event => ({
  id: event.context.params!.id,
  name: 'SST Homecoming 2024',
  description: 'SST Homecoming 2024',
  location: 'SST',
  badgeImage: 'https://www.sst.edu.sg/images/default-source/album/2019-2020/2020-01-24-homecoming/20200124_182000.jpg?sfvrsn=2',
  startDateTime: dayjs(Date.now()).valueOf(),
  endDateTime: dayjs(Date.now()).valueOf(),
  attendees: [
    {
      id: '123',
      name: 'Qin Guan',
      admissionKey: '123',
    },
  ],
}))
