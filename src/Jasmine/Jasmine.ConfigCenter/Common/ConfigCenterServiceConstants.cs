namespace Jasmine.ConfigCenter.Common
{
    public  class ConfigCenterServiceConstants
    {
        /// <summary>
        /// <para name="path">path</para>
        /// <para>nodetype<see cref="NodeType"/></para>
        /// return  <see cref="NodeOperateResult.Successced"/>or<see cref="NodeOperateResult.AlreadyExist"/>
        /// </summary>
        public const string CREATE_NODE = "create_node";
        /// <summary>
        /// <para name="path">path</para>
        /// return  <see cref="NodeOperateResult.Successced"/>or<see cref="NodeOperateResult.NotExist"/>
        /// </summary>
        public const string REMOVE_NODE = "remove_node";
        /// <summary>
        /// <para name="path">path <see cref="string"/></para>
        /// <para name="data">data <see cref="byte[]"/></para>
        /// return  <see cref="NodeOperateResult.Successced"/>or<see cref="NodeOperateResult.NotExist"/>
        /// </summary>
        public const string SET_DATA = "set-data";
        /// <summary>
        /// <para name="path">path <see cref="string"/></para>
        /// <para name="data">data <see cref="string"/></para>
        /// return <see cref="byte[]"/> byte[]
        /// </summary>
        public const string GET_DATA = "get_data";
        /// <summary>
        /// path
        /// </summary>
        public const string SUBSCRIBE_DATACHANGED = "subscribe_data_changed";
        /// <summary>
        /// path
        /// </summary>
        public const string UNSUBSCRIBE_DATACHANGED = "unsubscribe_data_changed";
        /// <summary>
        /// path
        /// </summary>
        public const string SUBSCRIBE_NODEREMOVED = "subscribe_node_removed";
        /// <summary>
        /// path
        /// </summary>
        public const string UNSUBSCRIBE_NODEREMOVED = "unsubscribe_node_removed";
        /// <summary>
        /// path
        /// </summary>
        public const string SUNSCRIBE_CHILDRENCREATED = "subscribe_children_created";
        /// <summary>
        /// path
        /// </summary>
        public const string UNSUBSCRIBE_CHILDRENCREATED = "unsubscribe_children_created";
    }
}
