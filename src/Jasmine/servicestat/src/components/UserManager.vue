<template>
  <div>
    <Table :columns="columns1" :data='datas1'></Table>
  </div>
</template>
<script>
export default {
  data() {
    return {
      columns1: [
        {
          title: "用户名",
          key: "name"
        },
        {
          title: "权限",
          key: "level"
        }
      ],
      datas1: [
        {
          name: "fal",
          level: "admin"
        }
      ]
    };
  }
};
</script>
