<script setup lang="ts">
import { f7BlockTitle, f7Link, f7List, f7ListButton, f7ListInput, f7ListItem, f7NavRight, f7NavTitle, f7NavTitleLarge, f7Navbar, f7Page, f7Popup, f7View } from 'framework7-vue'
import dayjs from 'dayjs'
import { devDependencies } from '~/package.json'

const appConfig = useAppConfig()

const { data: user } = useWhoAmI()
const graduationYear = computed(() => {
  return `Class of ${user.value?.member?.alumniMember?.graduationYear ?? user.value?.member?.employeeMember?.graduationYear}`
})

const dependencies = Object.keys(devDependencies)

const { mutate: userSignOutMutate } = useUserSignOutMutation()
</script>

<template>
  <f7Page>
    <f7Navbar large transparent :sliding="false">
      <f7NavTitle sliding>
        Profile
      </f7NavTitle>
      <f7NavTitleLarge>
        Profile
      </f7NavTitleLarge>
    </f7Navbar>

    <f7List v-if="user">
      <f7ListInput label="Name" type="text" placeholder="Your name" disabled :value="user.name" />
      <f7ListInput label="Email" type="Email" placeholder="Your email" disabled :value="user.email" />
      <f7ListInput label="Graduation year" placeholder="Your graduation year" disabled :value="graduationYear" />
      <f7ListItem>
        <span class="opacity-80 text-xs">
          To update this information, contact us at <a href="mailto:app@sstaa.org">app@sstaa.org</a>
        </span>
      </f7ListItem>
    </f7List>

    <f7List inset strong>
      <f7ListButton color="red" @click="userSignOutMutate()">
        Sign out
      </f7ListButton>
      <f7ListButton color="red">
        Delete and unlink my account
      </f7ListButton>
    </f7List>

    <f7List inset strong>
      <f7ListButton popup-open="#acknowledgements">
        Acknowledgements
      </f7ListButton>
    </f7List>

    <f7List>
      <f7ListItem class="opacity-80 text-sm">
        v{{ appConfig.buildInfo.version }}
        <br>
        <br>
        Built on {{ dayjs(appConfig.buildInfo.time).format('DD/MM/YYYY HH:mm') }}
        <br>
        Commit: {{ appConfig.buildInfo.shortCommit }}
      </f7ListItem>
    </f7List>

    <f7Popup id="acknowledgements" push>
      <f7View>
        <f7Page>
          <f7Navbar title="Acknowledgements" large transparent>
            <f7NavRight>
              <f7Link popup-close>
                Close
              </f7Link>
            </f7NavRight>
          </f7Navbar>

          <f7BlockTitle>Open source</f7BlockTitle>
          <f7List inset strong>
            <f7ListItem
              target="_blank" link="https://github.com/sstalumniassociation/web" external
              title="View on GitHub"
            />
          </f7List>

          <f7BlockTitle>Development team</f7BlockTitle>
          <f7List inset strong>
            <f7ListItem target="_blank" link="https://github.com/qin-guan" external title="Qin Guan" />
            <f7ListItem target="_blank" link="https://github.com/jiachenyee" external title="Jia Chen" />
            <f7ListItem target="_blank" link="https://github.com/arashnrim" external title="Arash" />
            <f7ListItem target="_blank" link="https://github.com/Ethan-Chew" external title="Ethan" />
          </f7List>

          <f7BlockTitle>Packages & libraries</f7BlockTitle>
          <f7List inset strong>
            <f7ListItem
              v-for="dependency in dependencies" :key="dependency" target="_blank"
              :link="`https://npmjs.com/package/${dependency}`" external :title="dependency"
            />
          </f7List>
        </f7Page>
      </f7View>
    </f7Popup>
  </f7Page>
</template>
