<script setup lang="ts">
import dayjs from 'dayjs'
import { devDependencies } from '~/package.json'

const appConfig = useAppConfig()

const { data: user } = useUser()
const graduationYear = computed(() => {
  return `Class of ${user.value?.graduationYear}`
})

const dependencies = Object.keys(devDependencies)

const { mutate: userSignOutMutate } = useUserSignOutMutation()
</script>

<template>
  <F7Page>
    <F7Navbar large transparent :sliding="false">
      <F7NavTitle sliding>
        Profile
      </F7NavTitle>
      <F7NavTitleLarge>
        Profile
      </F7NavTitleLarge>
    </F7Navbar>

    <F7List v-if="user">
      <F7ListInput label="Name" type="text" placeholder="Your name" disabled :value="user.name" />
      <F7ListInput label="Email" type="Email" placeholder="Your email" disabled :value="user.email" />
      <F7ListInput label="Graduation year" placeholder="Your graduation year" disabled :value="graduationYear" />
      <F7ListItem>
        <span class="opacity-80 text-xs">
          To update this information, contact us at <a href="mailto:app@sstaa.org">app@sstaa.org</a>
        </span>
      </F7ListItem>
    </F7List>

    <F7List inset strong>
      <F7ListButton color="red" @click="userSignOutMutate()">
        Sign out
      </F7ListButton>
      <F7ListButton color="red">
        Delete and unlink my account
      </F7ListButton>
    </F7List>

    <F7List inset strong>
      <F7ListButton popup-open="#acknowledgements">
        Acknowledgements
      </F7ListButton>
    </F7List>

    <F7List>
      <F7ListItem class="opacity-80 text-sm">
        v{{ appConfig.buildInfo.version }}
        <br>
        <br>
        Built on {{ dayjs(appConfig.buildInfo.time).format('DD/MM/YYYY HH:mm') }}
        <br>
        Commit: {{ appConfig.buildInfo.shortCommit }}
      </F7ListItem>
    </F7List>

    <F7Popup id="acknowledgements" push>
      <F7View>
        <F7Page>
          <F7Navbar title="Acknowledgements" large transparent>
            <F7NavRight>
              <F7Link popup-close>
                Close
              </F7Link>
            </F7NavRight>
          </F7Navbar>

          <F7BlockTitle>Open source</F7BlockTitle>
          <F7List inset strong>
            <F7ListItem
              target="_blank" link="https://github.com/sstalumniassociation/web" external
              title="View on GitHub"
            />
          </F7List>

          <F7BlockTitle>Development team</F7BlockTitle>
          <F7List inset strong>
            <F7ListItem target="_blank" link="https://github.com/qin-guan" external title="Qin Guan" />
            <F7ListItem target="_blank" link="https://github.com/jiachenyee" external title="Jia Chen" />
            <F7ListItem target="_blank" link="https://github.com/arashnrim" external title="Arash" />
            <F7ListItem target="_blank" link="https://github.com/Ethan-Chew" external title="Ethan" />
          </F7List>

          <F7BlockTitle>Packages & libraries</F7BlockTitle>
          <F7List inset strong>
            <F7ListItem
              v-for="dependency in dependencies" :key="dependency" target="_blank"
              :link="`https://npmjs.com/package/${dependency}`" external :title="dependency"
            />
          </F7List>
        </F7Page>
      </F7View>
    </F7Popup>
  </F7Page>
</template>
