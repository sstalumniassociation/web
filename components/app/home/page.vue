<script>
import {
  f7BlockTitle,
  f7Button,
  f7Icon,
  f7Link,
  f7List,
  f7ListItem,
  f7NavLeft,
  f7NavRight,
  f7NavTitle,
  f7NavTitleLarge,
  f7Navbar,
  f7Page,
  f7Searchbar,
  theme,
} from 'framework7-vue'

export default {
  components: {
    F7Page: f7Page,
    F7Navbar: f7Navbar,
    F7NavLeft: f7NavLeft,
    F7NavTitle: f7NavTitle,
    F7NavTitleLarge: f7NavTitleLarge,
    F7NavRight: f7NavRight,
    F7BlockTitle: f7BlockTitle,
    F7List: f7List,
    F7ListItem: f7ListItem,
    F7Link: f7Link,
    F7Searchbar: f7Searchbar,
    F7Icon: f7Icon,
    F7Button: f7Button,
  },
  props: {
    f7router: Object,
  },

  setup() {
    return {
      theme,
    }
  },
}
</script>

<template>
  <F7Page class="page-home">
    <F7Navbar large transparent :sliding="false" md-center-title>
      <F7NavLeft>
        <F7Link panel-open="left" icon-ios="f7:menu" icon-md="material:menu" />
      </F7NavLeft>
      <F7NavTitle sliding>
        Framework7 Vue
      </F7NavTitle>
      <F7NavRight>
        <F7Link
          class="searchbar-enable"
          data-searchbar=".searchbar-components"
          icon-ios="f7:search"
          icon-md="material:search"
        />
      </F7NavRight>
      <F7NavTitleLarge>Framework7 Vue</F7NavTitleLarge>
      <F7Searchbar
        class="searchbar-components"
        search-container=".components-list"
        search-in="a"
        expandable
      />
    </F7Navbar>

    <F7List strong inset dividers-ios class="searchbar-hide-on-search">
      <F7ListItem title="About Framework7" link="/about/">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
    </F7List>

    <F7BlockTitle class="searchbar-found">
      {{ $pwa.offlineReady }}
      {{ $pwa.needRefresh }}
    </F7BlockTitle>

    <F7Button @click="$pwa.updateServiceWorker()">
      reload
    </F7Button>

    <F7BlockTitle class="searchbar-found">
      Components
    </F7BlockTitle>
    <F7List strong inset dividers-ios class="components-list searchbar-found">
      <F7ListItem link="/app/events" title="Accordion">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/action-sheet/" title="Action Sheet">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/area-chart/" title="Area Chart">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/autocomplete/" title="Autocomplete">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/badge/" title="Badge">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/breadcrumbs/" title="Breadcrumbs">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/buttons/" title="Buttons">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/calendar/" title="Calendar / Date Picker">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/cards/" title="Cards">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/cards-expandable/" title="Cards Expandable">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/checkbox/" title="Checkbox">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/chips/" title="Chips/Tags">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/color-picker/" title="Color Picker">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/contacts-list/" title="Contacts List">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/content-block/" title="Content Block">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/data-table/" title="Data Table">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/dialog/" title="Dialog">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/fab/" title="FAB">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/fab-morph/" title="FAB Morph">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/form-storage/" title="Form Storage">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/gauge/" title="Gauge">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/grid/" title="Grid / Layout Grid">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/icons/" title="Icons">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/infinite-scroll/" title="Infinite Scroll">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/inputs/" title="Inputs">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/list-button/" title="List Button">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/list/" title="List View">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/list-index/" title="List Index">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/login-screen/" title="Login Screen">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/menu-list/" title="Menu List">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/messages/" title="Messages">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/navbar/" title="Navbar">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/notifications/" title="Notifications">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/panel/" title="Panel / Side Panels">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/photo-browser/" title="Photo Browser">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/picker/" title="Picker">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/pie-chart/" title="Pie Chart">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/popover/" title="Popover">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/popup/" title="Popup">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/preloader/" title="Preloader">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/progressbar/" title="Progress Bar">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/pull-to-refresh/" title="Pull To Refresh">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/radio/" title="Radio">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/range/" title="Range Slider">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/searchbar/" title="Searchbar">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/searchbar-expandable/" title="Searchbar Expandable">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/segmented/" title="Segmented">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/sheet-modal/" title="Sheet Modal">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/skeleton/" title="Skeleton (Ghost) Elements">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/smart-select/" title="Smart Select">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/sortable/" title="Sortable List">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/stepper/" title="Stepper">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/subnavbar/" title="Subnavbar">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/swipeout/" title="Swipeout (Swipe To Delete)">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/swiper/" title="Swiper Slider">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/tabs/" title="Tabs">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/text-editor/" title="Text Editor">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/timeline/" title="Timeline">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/toast/" title="Toast">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/toggle/" title="Toggle">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/toolbar-tabbar/" title="Toolbar & Tabbar">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/tooltip/" title="Tooltip">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/treeview/" title="Treeview">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
      <F7ListItem link="/virtual-list/" title="Virtual List">
        <template #media>
          <F7Icon icon="icon-f7" />
        </template>
      </F7ListItem>
    </F7List>
    <F7List strong inset dividers-ios class="searchbar-not-found">
      <F7ListItem title="Nothing found" />
    </F7List>
    <F7BlockTitle class="searchbar-hide-on-search">
      Themes
    </F7BlockTitle>
    <F7List strong inset dividers-ios class="searchbar-hide-on-search">
      <F7ListItem title="iOS Theme" external link="./?theme=ios" />
      <F7ListItem title="Material (MD) Theme" external link="./?theme=md" />
      <F7ListItem title="Color Themes" link="/color-themes/" />
    </F7List>

    <F7BlockTitle class="searchbar-hide-on-search">
      Page Loaders & Router
    </F7BlockTitle>
    <F7List strong inset dividers-ios class="searchbar-hide-on-search">
      <F7ListItem title="Page Transitions" link="/page-transitions/" />
      <F7ListItem title="Routable Modals" link="/routable-modals/" />
      <F7ListItem title="Default Route (404)" link="/load-something-that-doesnt-exist/" />
      <F7ListItem title="Master-Detail (Split View)" link="/master-detail/" />
      <F7ListItem title="Store" link="/store/" />
    </F7List>
  </F7Page>
</template>
