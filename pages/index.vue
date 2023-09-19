<script>
import {
  f7Block,
  f7Link,
  f7NavRight,
  f7Navbar,
  f7Page,
  f7Tab,
  f7Tabs,
  f7Toolbar,
} from 'framework7-vue'

export default {
  components: {
    F7Navbar: f7Navbar,
    F7Page: f7Page,
    F7Block: f7Block,
    F7Tabs: f7Tabs,
    F7Tab: f7Tab,
    F7Link: f7Link,
    F7Toolbar: f7Toolbar,
    F7NavRight: f7NavRight,
  },
  data() {
    return {
      toolbarPosition: 'bottom',
    }
  },
  methods: {
    toggleToolbarPosition() {
      this.toolbarPosition = this.toolbarPosition === 'top' ? 'bottom' : 'top'
    },
  },
}
</script>

<template>
  <F7Page :page-content="false">
    <F7Navbar title="Tabbar Icons">
      <F7NavRight>
        <F7Link
          icon-md="material:compare_arrows"
          icon-ios="f7:arrow_up_arrow_down_circle_fill"
          @click="toggleToolbarPosition"
        />
      </F7NavRight>
    </F7Navbar>
    <F7Toolbar position="bottom" tabbar icons>
      <F7Link
        tab-link="#tab-1"
        tab-link-active
        text="Tab 1"
        icon-ios="f7:envelope_fill"
        icon-md="material:email"
      />
      <F7Link
        tab-link="#tab-2"
        text="Tab 2"
        icon-ios="f7:calendar_fill"
        icon-md="material:today"
      />
      <F7Link
        tab-link="#tab-3"
        text="Tab 3"
        icon-ios="f7:cloud_upload_fill"
        icon-md="material:file_upload"
      />
    </F7Toolbar>

    <F7Tabs>
      <F7Tab id="tab-1" class="page-content" tab-active>
        <F7Block>
          <p>Tab 1 content</p>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ullam enim quia molestiae
            facilis laudantium voluptates obcaecati officia cum, sit libero commodi. Ratione illo
            suscipit temporibus sequi iure ad laboriosam accusamus?
          </p>
          <p>
            Saepe explicabo voluptas ducimus provident, doloremque quo totam molestias! Suscipit
            blanditiis eaque exercitationem praesentium reprehenderit, fuga accusamus possimus sed,
            sint facilis ratione quod, qui dignissimos voluptas! Aliquam rerum consequuntur
            deleniti.
          </p>
          <p>
            Totam reprehenderit amet commodi ipsum nam provident doloremque possimus odio itaque,
            est animi culpa modi consequatur reiciendis corporis libero laudantium sed eveniet unde
            delectus a maiores nihil dolores? Natus, perferendis.
          </p>
          <p>
            Atque quis totam repellendus omnis alias magnam corrupti, possimus aspernatur
            perspiciatis quae provident consequatur minima doloremque blanditiis nihil maxime
            ducimus earum autem. Magni animi blanditiis similique iusto, repellat sed quisquam!
          </p>
          <p>
            Suscipit, facere quasi atque totam. Repudiandae facilis at optio atque, rem nam, natus
            ratione cum enim voluptatem suscipit veniam! Repellat, est debitis. Modi nam mollitia
            explicabo, unde aliquid impedit! Adipisci!
          </p>
          <p>
            Deserunt adipisci tempora asperiores, quo, nisi ex delectus vitae consectetur iste
            fugiat iusto dolorem autem. Itaque, ipsa voluptas, a assumenda rem, dolorum porro
            accusantium, officiis veniam nostrum cum cumque impedit.
          </p>
          <p>
            Laborum illum ipsa voluptatibus possimus nesciunt ex consequatur rem, natus ad
            praesentium rerum libero consectetur temporibus cupiditate atque aspernatur, eaque
            provident eligendi quaerat ea soluta doloremque. Iure fugit, minima facere.
          </p>
        </F7Block>
      </F7Tab>
      <F7Tab id="tab-2" class="page-content">
        <F7Block>
          <p>Tab 2 content</p>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ullam enim quia molestiae
            facilis laudantium voluptates obcaecati officia cum, sit libero commodi. Ratione illo
            suscipit temporibus sequi iure ad laboriosam accusamus?
          </p>
          <p>
            Saepe explicabo voluptas ducimus provident, doloremque quo totam molestias! Suscipit
            blanditiis eaque exercitationem praesentium reprehenderit, fuga accusamus possimus sed,
            sint facilis ratione quod, qui dignissimos voluptas! Aliquam rerum consequuntur
            deleniti.
          </p>
          <p>
            Totam reprehenderit amet commodi ipsum nam provident doloremque possimus odio itaque,
            est animi culpa modi consequatur reiciendis corporis libero laudantium sed eveniet unde
            delectus a maiores nihil dolores? Natus, perferendis.
          </p>
          <p>
            Atque quis totam repellendus omnis alias magnam corrupti, possimus aspernatur
            perspiciatis quae provident consequatur minima doloremque blanditiis nihil maxime
            ducimus earum autem. Magni animi blanditiis similique iusto, repellat sed quisquam!
          </p>
          <p>
            Suscipit, facere quasi atque totam. Repudiandae facilis at optio atque, rem nam, natus
            ratione cum enim voluptatem suscipit veniam! Repellat, est debitis. Modi nam mollitia
            explicabo, unde aliquid impedit! Adipisci!
          </p>
          <p>
            Deserunt adipisci tempora asperiores, quo, nisi ex delectus vitae consectetur iste
            fugiat iusto dolorem autem. Itaque, ipsa voluptas, a assumenda rem, dolorum porro
            accusantium, officiis veniam nostrum cum cumque impedit.
          </p>
          <p>
            Laborum illum ipsa voluptatibus possimus nesciunt ex consequatur rem, natus ad
            praesentium rerum libero consectetur temporibus cupiditate atque aspernatur, eaque
            provident eligendi quaerat ea soluta doloremque. Iure fugit, minima facere.
          </p>
        </F7Block>
      </F7Tab>
      <F7Tab id="tab-3" class="page-content">
        <F7Block>
          <p>Tab 3 content</p>
          <p>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ullam enim quia molestiae
            facilis laudantium voluptates obcaecati officia cum, sit libero commodi. Ratione illo
            suscipit temporibus sequi iure ad laboriosam accusamus?
          </p>
          <p>
            Saepe explicabo voluptas ducimus provident, doloremque quo totam molestias! Suscipit
            blanditiis eaque exercitationem praesentium reprehenderit, fuga accusamus possimus sed,
            sint facilis ratione quod, qui dignissimos voluptas! Aliquam rerum consequuntur
            deleniti.
          </p>
          <p>
            Totam reprehenderit amet commodi ipsum nam provident doloremque possimus odio itaque,
            est animi culpa modi consequatur reiciendis corporis libero laudantium sed eveniet unde
            delectus a maiores nihil dolores? Natus, perferendis.
          </p>
          <p>
            Atque quis totam repellendus omnis alias magnam corrupti, possimus aspernatur
            perspiciatis quae provident consequatur minima doloremque blanditiis nihil maxime
            ducimus earum autem. Magni animi blanditiis similique iusto, repellat sed quisquam!
          </p>
          <p>
            Suscipit, facere quasi atque totam. Repudiandae facilis at optio atque, rem nam, natus
            ratione cum enim voluptatem suscipit veniam! Repellat, est debitis. Modi nam mollitia
            explicabo, unde aliquid impedit! Adipisci!
          </p>
          <p>
            Deserunt adipisci tempora asperiores, quo, nisi ex delectus vitae consectetur iste
            fugiat iusto dolorem autem. Itaque, ipsa voluptas, a assumenda rem, dolorum porro
            accusantium, officiis veniam nostrum cum cumque impedit.
          </p>
          <p>
            Laborum illum ipsa voluptatibus possimus nesciunt ex consequatur rem, natus ad
            praesentium rerum libero consectetur temporibus cupiditate atque aspernatur, eaque
            provident eligendi quaerat ea soluta doloremque. Iure fugit, minima facere.
          </p>
        </F7Block>
      </F7Tab>
    </F7Tabs>
  </F7Page>
</template>
