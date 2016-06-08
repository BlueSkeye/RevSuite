This set of interfaces is to be implemented by plugins. The interfaces are
actionned by the RSCore implementation. A plugin developper is expected to
reference this assembly. An RSCore client can safely ignore this library.
Actually the client will never see those interfaces and is not expected to
invoke them.