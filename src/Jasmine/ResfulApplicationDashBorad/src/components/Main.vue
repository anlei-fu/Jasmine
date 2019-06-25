<style scoped>
.layout {
  border: 1px solid #d7dde4;
  background: #f5f7f9;
  position: relative;
  border-radius: 4px;
  overflow: hidden;
}
.layout-logo {
  width: 100px;
  height: 30px;
  background: #5b6270;
  border-radius: 3px;
  float: left;
  position: relative;
  top: 15px;
  left: 20px;
}
.layout-nav {
  width: 420px;
  margin: 0 auto;
  margin-right: 20px;
}
.left-menu {
  text-align: left;
}
.menu-group {
  margin-top: 10px;
  font-size: 1.15em;
}
.drowdowm-a {
  text-decoration: none;
  font-size: 12px;
  margin-left: 10px;
}
</style>
<template>
  <div class="main">
    <!-- top container -->
    <Layout style="min-height:1000px">
      <!-- top header -->
      <Header>
        <div class="layout-logo"><img src="https://ss0.bdstatic.com/5aV1bjqh_Q23odCf/static/superman/img/logo_top_86d58ae1.png"/></div>

        <div class="layout-nav">
          <Dropdown>
            <a href="javascript:void(0)">
              <Icon type="ios-arrow-down"></Icon>更换主题
            </a>
            <DropdownMenu slot="list" style="magin-left:20px;">
              <DropdownItem>Drak</DropdownItem>
              <DropdownItem>Light</DropdownItem>
            </DropdownMenu>
          </Dropdown>

          <Dropdown style="magin-left:20px;">
            <a href="javascript:void(0)">
              <Icon type="ios-arrow-down"></Icon>语言
            </a>
            <DropdownMenu slot="list">
              <DropdownItem>中文</DropdownItem>
              <DropdownItem>英文</DropdownItem>
            </DropdownMenu>
          </Dropdown>

          <Dropdown style="magin-left:20px;">
            <a href="javascript:void(0)">
              <Icon type="ios-arrow-down"></Icon>
              {{this.$store.state.user.name}}
            </a>
            <DropdownMenu slot="list">
              <DropdownItem>修改密码</DropdownItem>
              <DropdownItem>退出</DropdownItem>
            </DropdownMenu>
          </Dropdown>
        </div>
      </Header>

      <!-- body --->
      <Layout>
        <!-- body-left -->
        <Sider hide-trigger width="200" :style="{background: '#fff'}">
          <Menu
            class="left-menu"
            active-name="1-2"
            theme="light"
            width="auto"
            :open-names="['1']"
            @on-select="menuSelectChangedCallback"
          >
            <MenuItem name="1">
              <Icon type="ios-pulse" :size="22"/>摘要
            </MenuItem>

            <!-- services manageinit all service by given serviceGroups[group{[service]}] -->
            <Submenu class="menu-group" name="2">
              <template slot="title">
                <Icon type="ios-cloud" :size="22"></Icon>Api管理
              </template>
              <Submenu v-for="(group,g_index) in this.$store.state.groups" v-bind:key="group.Name">
                <template slot="title">
                  <Icon type="ios-navigate" :size="22"></Icon>
                  {{group.Name}}
                </template>
                <MenuItem
                  v-on:click="showServiceDetail(null)"
                  v-bind:name="3+g_index+index"
                  v-for="(service,index ) in group.services"
                  v-bind:key="service.Name"
                >{{service.Name}}</MenuItem>
              </Submenu>
            </Submenu>

            <!-- sys api -->
            <MenuItem name="3">
              <Icon type="ios-infinite" :size="22"></Icon>系统服务
            </MenuItem>
            <!-- user manage-->
            <MenuItem name="4">
              <Icon type="ios-people" :size="22"/>用戶管理
            </MenuItem>
            <!-- about -->
            <MenuItem name="5" @click="showAbout">
              <Icon type="ios-bulb" :size="22"></Icon>关于
            </MenuItem>
          </Menu>
        </Sider>

        <!-- body-right -->
        <Layout :style="{padding: '0 24px 24px'}">
          <!-- <Breadcrumb :style="{margin: '24px 0'}">
            <BreadcrumbItem>Home</BreadcrumbItem>
            <BreadcrumbItem>Components</BreadcrumbItem>
            <BreadcrumbItem>Layout</BreadcrumbItem>
          </Breadcrumb>-->

          <!-- content -->
          <Content :style="{padding: '24px', minHeight: '280px', background: '#fff'}">
            <div id="sub-router">
              <router-view/>
            </div>
          </Content>
        </Layout>
      </Layout>
    </Layout>
  </div>
</template>
<script>
export default {
  name: "Main",
  data() {
    return {};
  },
  methods: {
    showAbout() {},
    menuSelectChangedCallback(name) {
      if (name === "1") this.$router.push({ path: "/main/summary" });
      else if (name === "2") this.$router.push({ path: "/main/service" });
      else if (name === "3") this.$router.push({ path: "/main/systemapi" });
      else if (name === "4") this.$router.push({ path: "/main/group" });
      else if (name === "5") this.$router.push({ path: "/main/service" });
    },
    showServiceDetail(service) {
      this.$store.state.updateCurrentService(service);

      this.$router.push({ path: "/srv" });
    },
    showGroupDetail(group) {},
    showUserManage() {},
    showSystemService() {},
    showSummary() {}
  }
};
</script>
