import dayjs from 'dayjs'
import RelativeTime from 'dayjs/plugin/relativeTime'
import LocalizedFormat from 'dayjs/plugin/localizedFormat'

export const useDayjs = createSharedComposable(() => {
  dayjs.extend(RelativeTime)
  dayjs.extend(LocalizedFormat)

  return dayjs
})
