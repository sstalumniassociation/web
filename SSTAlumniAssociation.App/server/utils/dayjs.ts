import _dayjs from 'dayjs'
import RelativeTime from 'dayjs/plugin/relativeTime'

_dayjs.extend(RelativeTime)

export const dayjs = _dayjs
